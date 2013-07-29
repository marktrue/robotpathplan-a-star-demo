#include "PathPlan.h"
#include "FibHeap.h"
#include <stdio.h>
#include <queue>
#include <cmath>
#include <memory.h>

#ifdef _DEBUG
#include <assert.h>
#define INVALID_CHECK_DEBUG 1
#else
#define INVALID_CHECK_DEBUG 0
#endif

//f(x)+g(x)*thita 用于调试，正常情况下应该为一
#define Thita 1

//动态寻路优化开关
#define DYNAMIC_OPTIMIZE 1

//斐波那契堆优化开关，因为问题的规模并不是很大，斐波那契堆较二叉堆而言，时间效率反而较差
#define FIBHEAP_OPTIMIZE 1

//原始A星算法OPEN表使用快速排序排序
#define OPEN_USE_LIST_QSORT 0 && !FIBHEAP_OPTIMIZE

//启发函数的优化
#define HEURISTIC_OPTIMIZE 1

#if OPEN_USE_LIST_QSORT
#include <vector>
#include <algorithm>
#endif

using namespace std;

#if HEURISTIC_OPTIMIZE
typedef unsigned int KEY_T;
#else
typedef float KEY_T;
#endif

struct node
{
	int x,y;
	int depth;
	KEY_T val;
	node* father;
	node* next;
#if FIBHEAP_OPTIMIZE
	unsigned int fibMark;
#endif
};

node** m_nodeMap;
BYTE** flag;
int g_nWidth = 0;
int g_nHeight = 0;
BYTE* g_bMap = NULL;
point g_pntStart = {0,0};
point g_pntEnd = {0,0};
bool m_nInitialized = false;
int *g_aPath = NULL;


struct cmp
{
	bool operator ()(const std::pair<point,KEY_T> &left,const std::pair<point,KEY_T> &right)
	{
		return left.second > right.second;
	}
};

inline KEY_T judge(point &p, point &EndP)
{
#if HEURISTIC_OPTIMIZE
	return abs(p.x - EndP.x) + abs(p.y - EndP.y);
#else
	float dx = p.x - EndP.x;
	float dy = p.y - EndP.y;
	return sqrt(dx * dx + dy * dy);
#endif
}

inline bool trial(int x, int y)
{
	if ( x < 0 || x >= g_nWidth || y < 0 || y >= g_nHeight || g_bMap[x + y * g_nWidth] == PntType_Wall || flag[x][y] == 2)
	{
		return false;
	}
	return true;
}

int FindPath(point &startP, point &endP, int *&aPath, int &nLen)
{
#if FIBHEAP_OPTIMIZE
	FibonaciiHeap<KEY_T,point> oPrtyQue;
#elif OPEN_USE_LIST_QSORT
	vector<point> oPrtyQue;
#else
	priority_queue<std::pair<point,KEY_T>,vector<std::pair<point,KEY_T>>,cmp> oPrtyQue;
#endif
	point oPntCur,oNewPnt;
	node *pNodeCur,*pNewNode;

	memset(flag[0], 0, g_nWidth * g_nHeight * sizeof(BYTE));
	m_nodeMap[startP.x][startP.y].depth = 0;
	m_nodeMap[startP.x][startP.y].val = judge(startP, endP);
	m_nodeMap[startP.x][startP.y].father = NULL;
	m_nodeMap[startP.x][startP.y].x = startP.x;
	m_nodeMap[startP.x][startP.y].y = startP.y;
	//m_nodeMap[startP.x][startP.y].next = NULL;
#if FIBHEAP_OPTIMIZE
	m_nodeMap[startP.x][startP.y].fibMark = oPrtyQue.insert(m_nodeMap[startP.x][startP.y].val, startP);
#elif OPEN_USE_LIST_QSORT
	oPrtyQue.push_back(startP);
#else
	oPrtyQue.push(std::make_pair(startP,m_nodeMap[startP.x][startP.y].val));
#endif
	flag[startP.x][startP.y] = 1;
	while ( !oPrtyQue.empty() )
	{
#if FIBHEAP_OPTIMIZE
		oPntCur = oPrtyQue.top().val;
		oPrtyQue.pop();
#elif OPEN_USE_LIST_QSORT
		oPntCur = oPrtyQue.back();
		oPrtyQue.pop_back();
#else
		oPntCur = oPrtyQue.top().first;
		oPrtyQue.pop();
#endif
		pNodeCur = &m_nodeMap[oPntCur.x][oPntCur.y];

#if INVALID_CHECK_DEBUG
		assert(oPntCur.x >= 0);
		assert(oPntCur.x < g_nWidth);
		assert(oPntCur.y >= 0);
		assert(oPntCur.y < g_nHeight);
#endif
		//
		if (flag[oPntCur.x][oPntCur.y] == 2)
		{
			continue;
		}
		flag[oPntCur.x][oPntCur.y] = 2;
		//
#if DYNAMIC_OPTIMIZE
		if (pNodeCur->next != NULL)
		{
			node *pNodeTmp,*pNodeDst = &m_nodeMap[endP.x][endP.y];
			int x,y,depth = pNodeCur->depth;
			for (pNodeTmp = pNodeCur; pNodeTmp != pNodeDst && pNodeTmp != NULL; pNodeTmp = pNodeTmp->next, depth++)
			{
				x = pNodeTmp->x;
				y = pNodeTmp->y;
				if (g_bMap[x + y * g_nWidth] == PntType_Wall)
				{
					break;
				}
				pNodeTmp->depth = depth;
				if (pNodeTmp->next != NULL)
				{
					pNodeTmp->next->father = pNodeTmp;
				}
			}
			if (pNodeTmp == pNodeDst)
			{
				//当已经有搜索过的路径到达终点时,则直接输出结果
				pNodeCur = pNodeDst;
				oPntCur = endP;
				pNodeTmp->depth = depth;
			}
			else
			{
				/*
				pNodeDst = pNodeTmp;
				//若这条路径已经过时(存在障碍物,或者不到达终点),则清除障碍物之前的路径
				for (pNodeTmp = pNodeCur; pNodeTmp != pNodeDst && pNodeTmp != NULL; pNodeTmp = pNewNode)
				{
					pNewNode = pNodeTmp->next;
					pNodeTmp->next = NULL;
				}
				*/
			}
		}
#endif
		//
		if (oPntCur.x == endP.x && oPntCur.y == endP.y)
		{
			//find the goal point
			nLen = pNodeCur->depth * 2;
#if INVALID_CHECK_DEBUG
			assert(g_aPath == NULL);
#endif
			g_aPath = new int[nLen];
			for (int i = nLen - 2; pNodeCur != NULL && i >= 0; pNodeCur = pNodeCur->father, i -= 2)
			{
				g_aPath[i] = (pNodeCur - m_nodeMap[0]) / g_nHeight;
				g_aPath[i + 1] = (pNodeCur - m_nodeMap[0]) % g_nHeight;
				//
				if (pNodeCur->father != NULL)
				{
					pNodeCur->father->next = pNodeCur;
				}
			}
			//while( !oPrtyQue.empty() ){oPrtyQue.pop();}
#if INVALID_CHECK_DEBUG
			assert(aPath ==NULL);
#endif
			aPath = g_aPath;
			return RPP_SUCCESS;
		}
		if (trial(oPntCur.x + 1, oPntCur.y))
		{
			oNewPnt = oPntCur;
			oNewPnt.x ++;
#if INVALID_CHECK_DEBUG
			assert(oNewPnt.x >= 0);
			assert(oNewPnt.x < g_nWidth);
			assert(oNewPnt.y >= 0);
			assert(oNewPnt.y < g_nHeight);
#endif
			pNewNode = &m_nodeMap[oNewPnt.x][oNewPnt.y];

			if (flag[oNewPnt.x][oNewPnt.y] == 0 || pNewNode->depth > pNodeCur->depth + 1)
			{
				pNewNode->x = oNewPnt.x;
				pNewNode->y = oNewPnt.y;
				pNewNode->father = pNodeCur;
				pNewNode->depth = pNodeCur->depth + 1;
				pNewNode->val = judge(oNewPnt, endP) + pNodeCur->depth * Thita;
				pNewNode->next = NULL;

#if FIBHEAP_OPTIMIZE
				if (flag[oNewPnt.x][oNewPnt.y] == 0)
				{
					pNewNode->fibMark = oPrtyQue.insert(pNewNode->val, oNewPnt);
				}
				else
				{
					oPrtyQue.DecreaseKey(pNewNode->fibMark, pNewNode->val);
				}
#elif OPEN_USE_LIST_QSORT
				oPrtyQue.push_back(oNewPnt);
				sort(oPrtyQue.begin(),oPrtyQue.end());
#else
				oPrtyQue.push(std::make_pair(oNewPnt,pNewNode->val));
#endif
				flag[oNewPnt.x][oNewPnt.y] = 1;
			}/*
			else if (flag[oNewPnt.x][oNewPnt.y] == 1 && pNewNode->depth > pNodeCur->depth + 1)
			{
				;
			}*/
		}
		if (trial(oPntCur.x - 1, oPntCur.y))
		{
			oNewPnt = oPntCur;
			oNewPnt.x --;
#if INVALID_CHECK_DEBUG
			assert(oNewPnt.x >= 0);
			assert(oNewPnt.x < g_nWidth);
			assert(oNewPnt.y >= 0);
			assert(oNewPnt.y < g_nHeight);
#endif
			pNewNode = &m_nodeMap[oNewPnt.x][oNewPnt.y];

			if (flag[oNewPnt.x][oNewPnt.y] == 0 || pNewNode->depth > pNodeCur->depth + 1)
			{
				pNewNode->x = oNewPnt.x;
				pNewNode->y = oNewPnt.y;
				pNewNode->father = pNodeCur;
				pNewNode->depth = pNodeCur->depth + 1;
				pNewNode->val = judge(oNewPnt, endP) + pNodeCur->depth * Thita;
				pNewNode->next = NULL;

#if FIBHEAP_OPTIMIZE
				if (flag[oNewPnt.x][oNewPnt.y] == 0)
				{
					pNewNode->fibMark = oPrtyQue.insert(pNewNode->val, oNewPnt);
				}
				else
				{
					oPrtyQue.DecreaseKey(pNewNode->fibMark, pNewNode->val);
				}
#elif OPEN_USE_LIST_QSORT
				oPrtyQue.push_back(oNewPnt);
				sort(oPrtyQue.begin(),oPrtyQue.end());
#else
				oPrtyQue.push(std::make_pair(oNewPnt,pNewNode->val));
#endif
				flag[oNewPnt.x][oNewPnt.y] = 1;
			}
		}
		if (trial(oPntCur.x, oPntCur.y + 1))
		{
			oNewPnt = oPntCur;
			oNewPnt.y ++;
#if INVALID_CHECK_DEBUG
			assert(oNewPnt.x >= 0);
			assert(oNewPnt.x < g_nWidth);
			assert(oNewPnt.y >= 0);
			assert(oNewPnt.y < g_nHeight);
#endif
			pNewNode = &m_nodeMap[oNewPnt.x][oNewPnt.y];

			if (flag[oNewPnt.x][oNewPnt.y] == 0 || pNewNode->depth > pNodeCur->depth + 1)
			{
				pNewNode->x = oNewPnt.x;
				pNewNode->y = oNewPnt.y;
				pNewNode->father = pNodeCur;
				pNewNode->depth = pNodeCur->depth + 1;
				pNewNode->val = judge(oNewPnt, endP) + pNodeCur->depth * Thita;
				pNewNode->next = NULL;

#if FIBHEAP_OPTIMIZE
				if (flag[oNewPnt.x][oNewPnt.y] == 0)
				{
					pNewNode->fibMark = oPrtyQue.insert(pNewNode->val, oNewPnt);
				}
				else
				{
					oPrtyQue.DecreaseKey(pNewNode->fibMark, pNewNode->val);
				}
#elif OPEN_USE_LIST_QSORT
				oPrtyQue.push_back(oNewPnt);
				sort(oPrtyQue.begin(),oPrtyQue.end());
#else
				oPrtyQue.push(std::make_pair(oNewPnt,pNewNode->val));
#endif
				flag[oNewPnt.x][oNewPnt.y] = 1;
			}
		}
		if (trial(oPntCur.x, oPntCur.y - 1))
		{
			oNewPnt = oPntCur;
			oNewPnt.y --;
#if INVALID_CHECK_DEBUG
			assert(oNewPnt.x >= 0);
			assert(oNewPnt.x < g_nWidth);
			assert(oNewPnt.y >= 0);
			assert(oNewPnt.y < g_nHeight);
#endif
			pNewNode = &m_nodeMap[oNewPnt.x][oNewPnt.y];

			if (flag[oNewPnt.x][oNewPnt.y] == 0 || pNewNode->depth > pNodeCur->depth + 1)
			{
				pNewNode->x = oNewPnt.x;
				pNewNode->y = oNewPnt.y;
				pNewNode->father = pNodeCur;
				pNewNode->depth = pNodeCur->depth + 1;
				pNewNode->val = judge(oNewPnt, endP) + pNodeCur->depth * Thita;
				pNewNode->next = NULL;

#if FIBHEAP_OPTIMIZE
				if (flag[oNewPnt.x][oNewPnt.y] == 0)
				{
					pNewNode->fibMark = oPrtyQue.insert(pNewNode->val, oNewPnt);
				}
				else
				{
					oPrtyQue.DecreaseKey(pNewNode->fibMark, pNewNode->val);
				}
#elif OPEN_USE_LIST_QSORT
				oPrtyQue.push_back(oNewPnt);
				sort(oPrtyQue.begin(),oPrtyQue.end());
#else
				oPrtyQue.push(std::make_pair(oNewPnt,pNewNode->val));
#endif
				flag[oNewPnt.x][oNewPnt.y] = 1;
			}
		}
	}
	return RPP_FAILED;
}

int ReleasePathArray()
{
	if (g_aPath != NULL)
	{
		delete[] g_aPath;
		g_aPath = NULL;
	}
	return RPP_SUCCESS;
}

int InitializePathPlan(int nWidth, int nHeight)
{
	if (m_nInitialized)
	{
		return RPP_FAILED;
	}
	//
	m_nodeMap = new node*[nWidth];
	if (m_nodeMap == NULL)
	{
		return RPP_FAILED;
	}
	//
	m_nodeMap[0] = new node[nWidth * nHeight];
	if (m_nodeMap[0] == NULL)
	{
		delete[] m_nodeMap;
		m_nodeMap = NULL;
		return RPP_FAILED;
	}
	//
	for (int j = 0; j < nHeight ; ++j)
	{
		m_nodeMap[0][j].next = NULL;
	}
	for (int i = 1; i < nWidth ; ++i)
	{
		m_nodeMap[i] = m_nodeMap[i - 1] + nHeight;
		for (int j = 0; j < nHeight ; ++j)
		{
			m_nodeMap[i][j].next = NULL;
		}
	}
	//
	flag = new BYTE*[nWidth];
	if (flag == NULL)
	{
		delete[] m_nodeMap[0];
		m_nodeMap[0] = NULL;
		delete[] m_nodeMap;
		m_nodeMap = NULL;
		return RPP_FAILED;
	}
	//
	flag[0] = new BYTE[nWidth * nHeight];
	if (flag[0] == NULL)
	{
		delete[] m_nodeMap[0];
		m_nodeMap[0] = NULL;
		delete[] m_nodeMap;
		m_nodeMap = NULL;
		//
		delete[] flag;
		flag = NULL;
		return RPP_FAILED;
	}
	//
	for (int i = 1; i < nWidth ; ++i)
	{
		flag[i] = flag[i - 1] + nHeight;
	}
	g_nWidth = nWidth;
	g_nHeight = nHeight;
	m_nInitialized = true;
	return RPP_SUCCESS;
}

int UnInitializePathPlan()
{
	if (!m_nInitialized)
	{
		return RPP_FAILED;
	}
	//assert(m_nodeMap[0] != NULL);
	//delete[] m_nodeMap[0];
#if INVALID_CHECK_DEBUG
	assert(m_nodeMap != NULL);
#endif
	delete[] m_nodeMap[0];
	m_nodeMap[0] = NULL;
	delete[] m_nodeMap;
	m_nodeMap = NULL;

	//assert(flag[0] != NULL);
	//delete[] flag[0];
#if INVALID_CHECK_DEBUG
	assert(flag != NULL);
#endif
	delete[] flag[0];
	flag[0] = NULL;
	delete[] flag;
	flag = NULL;

	m_nInitialized = false;
	return RPP_SUCCESS;
}
