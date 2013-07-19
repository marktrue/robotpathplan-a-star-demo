#include <stdio.h>
#include <memory.h>
#include <time.h>
#include <stdlib.h>
#include "RobotPathPlanDll.h"
#include "PathPlan.h"

int RobotPathPlan(int* &aPath, int &nLen)
{
	int nRet;
	FILE *pf_log = fopen("F://findpathlog.log","a+");
	clock_t t1,t2;
	t1 = clock();
	nRet = FindPath(g_pntStart, g_pntEnd, aPath, nLen);
	t2 = clock();
	fprintf(pf_log, "find path in time:%dms pathLen:%d\n",t2 - t1,nLen / 2);
	fclose(pf_log);
	return nRet;
}

int RobotLoadMap(char* sPath, int &nStartPx, int &nStartPy, int &nEndPx, int &nEndPy)
{
	FILE *pfile;
	UnInitializePathPlan();
	if (g_bMap != NULL)
	{
		delete[] g_bMap;
		g_bMap = NULL;
	}
	pfile = fopen(sPath, "rb");
	if (pfile == NULL)
	{
		return RPP_FILEOPENFAILED;
	}
	fread(&g_nWidth, sizeof(int), 1, pfile);
	fread(&g_nHeight, sizeof(int), 1, pfile);
	fread(&g_pntStart, sizeof(point), 1, pfile);
	fread(&g_pntEnd, sizeof(point), 1, pfile);
	nStartPx = g_pntStart.x;
	nStartPy = g_pntStart.y;
	nEndPx = g_pntEnd.x;
	nEndPy = g_pntEnd.y;
	g_bMap = new BYTE[g_nWidth * g_nHeight];
	if (g_bMap == NULL)
	{
		return RPP_FAILED;
	}
	fread(g_bMap, sizeof(BYTE), g_nWidth * g_nHeight, pfile);
	fclose(pfile);
	InitializePathPlan(g_nWidth, g_nHeight);
	return RPP_SUCCESS;
}

int RobotSaveMap(char* sPath)
{
	FILE *pfile;
	pfile = fopen(sPath, "wb");
	if (pfile == NULL)
	{
		return RPP_FILEOPENFAILED;
	}
	fwrite(&g_nWidth, sizeof(int), 1, pfile);
	fwrite(&g_nHeight, sizeof(int), 1, pfile);
	fwrite(&g_pntStart, sizeof(point), 1, pfile);
	fwrite(&g_pntEnd, sizeof(point), 1, pfile);
	fwrite(g_bMap, sizeof(BYTE), g_nWidth * g_nHeight, pfile);
	fclose(pfile);
	return RPP_SUCCESS;
}

int RobotCreateEmptyMap(int nWidth, int nHeight, int &nStartPx, int &nStartPy, int &nEndPx, int &nEndPy)
{
	UnInitializePathPlan();
	if (g_bMap != NULL)
	{
		delete[] g_bMap;
	}
	g_nWidth = nWidth;
	g_nHeight = nHeight;
	g_bMap = new BYTE[nWidth * nHeight];
	if (g_bMap == NULL)
	{
		return RPP_FAILED;
	}
	memset(g_bMap, PntType_Normal, nWidth * nHeight * sizeof(BYTE));
	//
	g_bMap[0] = PntType_Normal;
	g_bMap[nWidth * nHeight - 1] = PntType_Normal;
	g_pntStart.x = 0;
	g_pntStart.y = 0;
	g_pntEnd.x = nWidth - 1;
	g_pntEnd.y = nHeight - 1;
	nStartPx = nStartPy = 0;
	nEndPx = nWidth - 1;
	nEndPy = nHeight - 1;
	InitializePathPlan(g_nWidth, g_nHeight);
	//
	return RPP_SUCCESS;
}

int RobotCreateRandomMap(int nWidth, int nHeight, int &nStartPx, int &nStartPy, int &nEndPx, int &nEndPy, float fDensity)
{
	UnInitializePathPlan();
	if (g_bMap != NULL)
	{
		delete[] g_bMap;
	}
	g_nWidth = nWidth;
	g_nHeight = nHeight;
	g_bMap = new BYTE[nWidth * nHeight];
	if (g_bMap == NULL)
	{
		return RPP_FAILED;
	}
	//memset(g_bMap, PntType_Normal, nWidth * nHeight * sizeof(BYTE));
	//
	float fpos;
	srand(time(NULL));
	for (int i = 0; i < nWidth ; ++i)
	{
		for (int j = 0; j < nHeight ; ++j)
		{
			fpos = (rand() % 1001) / 1000.0;
			g_bMap[i + j * nWidth] = PntType_Normal;
			if (fpos < fDensity)
			{
				g_bMap[i + j * nWidth] = PntType_Wall;
			}
		}
	}
	//
	g_bMap[0] = PntType_Normal;
	g_bMap[nWidth * nHeight - 1] = PntType_Normal;
	g_pntStart.x = 0;
	g_pntStart.y = 0;
	g_pntEnd.x = nWidth - 1;
	g_pntEnd.y = nHeight - 1;
	nStartPx = nStartPy = 0;
	nEndPx = nWidth - 1;
	nEndPy = nHeight - 1;
	InitializePathPlan(g_nWidth, g_nHeight);
	return RPP_SUCCESS;
}

int setRobotPoint(int x, int y)
{
	//g_bMap[g_pntStart.x + g_pntStart.y * g_nWidth] = PntType_Normal;
	g_pntStart.x = x;
	g_pntStart.y = y;
	//g_bMap[g_pntStart.x + g_pntStart.y * g_nWidth] = PntType_Robot;
	return RPP_SUCCESS;
}

int setEndPoint(int x, int y)
{
	//g_bMap[g_pntEnd.x + g_pntEnd.y * g_nWidth] = PntType_Normal;
	g_pntEnd.x = x;
	g_pntEnd.y = y;
	//g_bMap[g_pntEnd.x + g_pntEnd.y * g_nWidth] = PntType_Goal;
	return RPP_SUCCESS;
}

int getMap(BYTE* &bMap)
{
	if (g_bMap == NULL)
	{
		return RPP_FAILED;
	}
	bMap = g_bMap;
	return RPP_SUCCESS;
}

int getWidth()
{
	return g_nWidth;
}

int getHeight()
{
	return g_nHeight;
}
