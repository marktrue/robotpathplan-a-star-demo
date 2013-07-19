//*************************************************************************************************************
//*	FibHeap.h
//*	FibonaciiHeap
//*	Author MarkTrue
//*	Email ahu.marktrue@gmail.com
//*	Modified for A star Algorithm
//*************************************************************************************************************
#ifndef FIBONACCIHEAP_VECTOR_H
#define FIBONACCIHEAP_VECTOR_H

//#define VARIFY_CIRCLE
//#define ASSERT_VARIFY

#include <stdio.h>
#include <stdlib.h>
#ifdef ASSERT_VARIFY
#include <assert.h>
#endif

#define INVALID -1

typedef unsigned int FIBHEAP_UINT;
typedef int FIBHEAP_INT;

template<class key_t, class val_t>
struct FibPair
{
	key_t				key;
	val_t				val;
};

//********************************************************************
//*	Ĭ���Ǹ�С����
//********************************************************************
template<class key_t>
struct defaultless
{
	bool operator()(const key_t &left, const key_t &right) const
	{
		return left < right;
	}
};

template<class key_t, class val_t,class _cmp = less<key_t>>
struct FibHeapNode
{
	//********************************************************************
	//*	���ڵ�ĳһ����Ů
	//********************************************************************
	FIBHEAP_UINT			child;

	//********************************************************************
	//*	���ڵ����ֵ�
	//********************************************************************
	FIBHEAP_UINT			left;

	//********************************************************************
	//*	���ڵ����ֵ�
	//********************************************************************
	FIBHEAP_UINT			right;

	//********************************************************************
	//*	���ڵ�ĸ��ڵ�
	//********************************************************************
	FIBHEAP_UINT			parent;

	//********************************************************************
	//*	���ڵ�Ĺؼ��ֺ����洢��ֵ
	//********************************************************************
	FibPair<key_t,val_t>	keyval;

	//********************************************************************
	//*	���ڵ���Ů����
	//********************************************************************
	FIBHEAP_UINT			degree;

	//********************************************************************
	//*	��ʾ���ڵ��Դ���һ�γ�Ϊ��һ���ڵ����Ů֮�����Ƿ�ʧ����һ������
	//********************************************************************
	bool					flag;

	//********************************************************************
	//*	��������ָ��
	//********************************************************************
	FIBHEAP_UINT			pRLink;
};

template<class key_t, class val_t,class _cmp = defaultless<key_t>>
class FibonaciiHeap
{
public:

	//֮����Ϊ64����Ϊ��֤ÿ���ڵ�Ķ���(degree)���ᳬ��floor(log2(nNodeNum)),
	//��nNodeNum������Ϊ64λ(��64λϵͳ��),�ʱ������ֻ��Ҫ������64����
	//32λϵͳ��ɵ�����32
#define	nMaxDegree	32
	typedef FibHeapNode<key_t, val_t, _cmp> node;

	FibonaciiHeap()
	{
		RecycleInit();
		pRoot = INVALID;
		nNodeNum = 0;
		nMarkedNum = 0;
		nTreeNum = 0;
	}

	~FibonaciiHeap()
	{
		RecycleUnInit();
	}
	
	//********************************************************************
	//*	����ؼ��ֺͶ�Ӧ��ֵ
	//*	���ش洢��λ��,�������DecreaseKey
	//********************************************************************
	FIBHEAP_UINT insert(const key_t &key, const val_t &val)
	{
		//node* _pNode = new node;
		FIBHEAP_UINT _pNode = newNode();
		pNodeSet[_pNode].keyval.key = key;
		pNodeSet[_pNode].keyval.val = val;
		pNodeSet[_pNode].degree = 0;
		pNodeSet[_pNode].parent = INVALID;
		pNodeSet[_pNode].child = INVALID;
		pNodeSet[_pNode].left = _pNode;
		pNodeSet[_pNode].right = _pNode;
		pNodeSet[_pNode].flag = false;
		// concatenate the root list containing _pNode with root list
		if (pRoot != INVALID)
		{
#ifdef VARIFY_CIRCLE
			varify(pRoot);
#endif
			pNodeSet[_pNode].left = pNodeSet[pRoot].left;
			pNodeSet[_pNode].right = pRoot;
			pNodeSet[pNodeSet[_pNode].left].right = _pNode;
			pNodeSet[pRoot].left = _pNode;
#ifdef VARIFY_CIRCLE
			varify(pRoot);
#endif
		}
		//
		if ( pRoot == INVALID || _cmp()(pNodeSet[_pNode].keyval.key, pNodeSet[pRoot].keyval.key) )
		{
			pRoot = _pNode;
#ifdef ASSERT_VARIFY
			assert(pRoot < nUpBound);
#endif
		}
		nNodeNum ++;
		nTreeNum ++;
		return _pNode;
	}
	
	//********************************************************************
	//*	���ضѶ�Ԫ��
	//********************************************************************
	FibPair<key_t, val_t> &top()
	{
#ifdef ASSERT_VARIFY
		assert(pRoot < nUpBound);
#endif
		return pNodeSet[pRoot].keyval;
	}
	
	//********************************************************************
	//*	����
	//********************************************************************
	void pop()
	{
		FIBHEAP_UINT _pNode = pRoot;
		FIBHEAP_UINT head,tail;
		if (_pNode != INVALID)
		{
			//
			if (pNodeSet[_pNode].degree != 0)
			{
				head = pNodeSet[_pNode].child;
				tail = pNodeSet[head].left;
#ifdef VARIFY_CIRCLE
				varify(tail);
#endif
				pNodeSet[head].left = pNodeSet[_pNode].left;
				pNodeSet[tail].right = _pNode;
				pNodeSet[pNodeSet[head].left].right = head;
				pNodeSet[_pNode].left = tail;
#ifdef VARIFY_CIRCLE
				varify(head);
#endif
				for (;head != _pNode;head = pNodeSet[head].right)
				{
					pNodeSet[head].parent = INVALID;
				}
				//
				nTreeNum += pNodeSet[_pNode].degree;
				pNodeSet[_pNode].degree = 0;
			}
			//
			// remove _pNode from the root list
			pNodeSet[pNodeSet[_pNode].left].right = pNodeSet[_pNode].right;
			pNodeSet[pNodeSet[_pNode].right].left = pNodeSet[_pNode].left;
			//
#ifdef VARIFY_CIRCLE
			varify(pNodeSet[_pNode].right);
#endif
			//
			nTreeNum --;
			nNodeNum --;
			if (pNodeSet[_pNode].right == _pNode)
			{
				pRoot = INVALID;
#ifdef ASSERT_VARIFY
				assert(nNodeNum == 0);
#endif
			}
			else
			{
				pRoot = pNodeSet[_pNode].right;
#ifdef ASSERT_VARIFY
				assert(pRoot < nUpBound);
#endif
				consolidate();
			}
			//delete _pNode;
			freeNode(_pNode);
		}
#ifdef ASSERT_VARIFY
		if (nNodeNum == 0)
		{
			assert(pRoot == INVALID);
		}
		else
		{
			assert(pRoot != INVALID);
		}
#endif
	}
	
	//********************************************************************
	//*	��С�ؼ���
	//*	__node	Ԫ��λ��
	//*	__key	��С��Ĺؼ���
	//********************************************************************
	void DecreaseKey(FIBHEAP_UINT __node, key_t __key)
	{
#ifdef ASSERT_VARIFY
		assert(__node < nUpBound);
#endif
		FIBHEAP_UINT y;
		if (__key > pNodeSet[__node].keyval.key)
		{
			//fatal error
			return;
		}
		pNodeSet[__node].keyval.key = __key;
		y = pNodeSet[__node].parent;
		if (y != INVALID && pNodeSet[__node].keyval.key < pNodeSet[y].keyval.key)
		{
			Cut(__node, y);
			CascadingCut(y);
		}
		if (pNodeSet[__node].keyval.key < pNodeSet[pRoot].keyval.key)
		{
			pRoot = __node;
#ifdef ASSERT_VARIFY
			assert(pRoot < nUpBound);
#endif
		}
	}
	
	//********************************************************************
	//*	����Ԫ�ظ���
	//********************************************************************
	FIBHEAP_UINT size()
	{
		return nNodeNum;
	}
	
	//********************************************************************
	//*	���Ƿ�Ϊ��
	//********************************************************************
	bool empty()
	{
		return nNodeNum == 0;
	}

private:

#ifdef VARIFY_CIRCLE
	//********************************************************************
	//*	�����Լ��
	//********************************************************************
	void varify(FIBHEAP_UINT x)
	{
		FIBHEAP_UINT tmp;
		for (tmp = x;pNodeSet[tmp].right != x; tmp = pNodeSet[tmp].right)
		{
			int p = 1;
		}
	}
#endif
	
	//********************************************************************
	//*	��y���ӽڵ���ɾ��x,����x���뵽���б���
	//********************************************************************
	void Cut(FIBHEAP_UINT x,FIBHEAP_UINT y)
	{
		// remove x from the child list of y, decrementing degree[y]
		if (pNodeSet[y].degree == 1)
		{
			pNodeSet[y].child = INVALID;
		}
		else
		{
#ifdef VARIFY_CIRCLE
			varify(x);
			FIBHEAP_UINT p = pNodeSet[x].right;
#endif
			if (pNodeSet[y].child == x)
			{
				pNodeSet[y].child = pNodeSet[x].right;
			}
			pNodeSet[pNodeSet[x].left].right = pNodeSet[x].right;
			pNodeSet[pNodeSet[x].right].left = pNodeSet[x].left;
#ifdef VARIFY_CIRCLE
			varify(p);
#endif
		}
		--pNodeSet[y].degree;
		//add x to the root list
		pNodeSet[x].right = pNodeSet[pRoot].right;
		pNodeSet[x].left = pRoot;
		pNodeSet[pNodeSet[x].right].left = x;
		pNodeSet[pRoot].right = x;
#ifdef VARIFY_CIRCLE
		varify(x);
#endif
		++nTreeNum;
		//parent[x] = NIL
		pNodeSet[x].parent = INVALID;
		pNodeSet[x].flag = false;
	}
	
	//********************************************************************
	//*	�����ж�y,�ж�y���丸�ڵ㣬����y�ӵ����ڵ�
	//********************************************************************
	void CascadingCut(FIBHEAP_UINT y)
	{
		FIBHEAP_UINT z = pNodeSet[y].parent;
		if (z != INVALID)
		{
			if (pNodeSet[y].flag == false)
			{
				pNodeSet[y].flag = true;
			}
			else
			{
				Cut(y,z);
				CascadingCut(z);
			}
		}
	}
	
	//********************************************************************
	//*	������,ά�����ݽṹ
	//********************************************************************
	void consolidate()
	{
		FIBHEAP_UINT i = 0,len = nTreeNum;
		FIBHEAP_UINT d;
		FIBHEAP_UINT max_degree = 0;
		FIBHEAP_UINT it,next;
		for (i = 0; i < nMaxDegree; i++)
		{
			pDegreeCount[i] = INVALID;
		}
		for (it = pRoot,i = 0; i < len; it = next,i++)
		{
			FIBHEAP_UINT x,y;
			x = it;
			next = pNodeSet[it].right;
			d = pNodeSet[x].degree;
			while (pDegreeCount[d] != INVALID)
			{
				y = pDegreeCount[d];
				if(_cmp()(pNodeSet[y].keyval.key, pNodeSet[x].keyval.key))
				{
					FIBHEAP_UINT t = x;
					x = y;
					y = t;
					it = x;
				}
				link(x, y);
				pDegreeCount[d] = INVALID;
				d++;
			}
			pDegreeCount[d] = x;
			//
			if (d > max_degree)
			{
				max_degree = d;
			}
		}
		pRoot = INVALID;
		for (i = 0; i <= max_degree ; i++)
		{
			if ( pDegreeCount[i] != INVALID && (pRoot == INVALID || _cmp()(pNodeSet[pDegreeCount[i]].keyval.key, pNodeSet[pRoot].keyval.key)) )
			{
				pRoot = pDegreeCount[i];
#ifdef ASSERT_VARIFY
				assert(pRoot < nUpBound);
#endif
			}
		}
	}
	
	//********************************************************************
	//*	��_src���ӵ�_dst���ӽڵ���
	//********************************************************************
	void link(FIBHEAP_UINT _dst, FIBHEAP_UINT _src)
	{
		//remove _src from the root list
#ifdef VARIFY_CIRCLE
		varify(_src);
#endif
		pNodeSet[pNodeSet[_src].left].right = pNodeSet[_src].right;
		pNodeSet[pNodeSet[_src].right].left = pNodeSet[_src].left;
		pNodeSet[_src].parent = _dst;
		//make _src a child of _dst, incrementing _src
		if (pNodeSet[_dst].child == INVALID)
		{
			pNodeSet[_dst].child = _src;
			pNodeSet[_src].left = _src;
			pNodeSet[_src].right = _src;
		}
		else
		{
#ifdef VARIFY_CIRCLE
			varify(pNodeSet[_dst].child);
#endif
			pNodeSet[_src].left = pNodeSet[pNodeSet[_dst].child].left;
			pNodeSet[_src].right = pNodeSet[_dst].child;
			pNodeSet[pNodeSet[_src].left].right = _src;
			pNodeSet[pNodeSet[_src].right].left = _src;
#ifdef VARIFY_CIRCLE
			varify(_src);
#endif
		}
		pNodeSet[_dst].degree ++;
		pNodeSet[_dst].flag = false;
		nTreeNum --;
	}
private:
	FIBHEAP_UINT						pRoot;
	FIBHEAP_UINT						nNodeNum;
	FIBHEAP_UINT						nTreeNum;
	FIBHEAP_UINT						nMarkedNum;
	//
	FIBHEAP_UINT						pDegreeCount[nMaxDegree];

	//********************************************************************
	//*	[5/17/2013 marktrue]
	//********************************************************************
private:

	node								*pNodeSet;
	FIBHEAP_UINT						pNodeRecycleLink;
	FIBHEAP_UINT						nUpBound;
	FIBHEAP_UINT						nAllocP;
	
	//********************************************************************
	//*	�ڴ���ճ�ʼ��
	//********************************************************************
	void RecycleInit()
	{
		pNodeRecycleLink = INVALID;
		nUpBound = 256;
		nAllocP = 0;
		pNodeSet = (node*)malloc(sizeof(node) * nUpBound);
	}
	
	//********************************************************************
	//*	�ڴ�������ʼ��
	//********************************************************************
	void RecycleUnInit()
	{
		free(pNodeSet);
	}

	FIBHEAP_UINT newNode()
	{
		FIBHEAP_UINT pNewNode;
		if (pNodeRecycleLink != INVALID)
		{
			pNewNode = pNodeRecycleLink;
			pNodeRecycleLink = pNodeSet[pNewNode].pRLink;
			return pNewNode;
		}
		if (nAllocP >= nUpBound)
		{
			//realloc
			//memory grow
			nUpBound = nUpBound << 1;
			if ((FIBHEAP_INT)nUpBound < 0)
			{
				//fatal error
				return INVALID;
			}
			pNodeSet = (node*)realloc(pNodeSet, sizeof(node) * nUpBound);
			if (pNodeSet == NULL)
			{
				//fatal error
				return INVALID ;
			}
		}
		//
		pNewNode = nAllocP;
		nAllocP ++;
		return pNewNode;
	}

	void freeNode(FIBHEAP_UINT pNodeCur)
	{
		pNodeSet[pNodeCur].pRLink = pNodeRecycleLink;
		pNodeRecycleLink = pNodeCur;
	}

#undef nMaxDegree
};

#endif