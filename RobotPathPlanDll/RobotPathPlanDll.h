#ifndef ROBOTPATHPLANDLL_H
#define ROBOTPATHPLANDLL_H

#ifdef ROBOTPATHPLANDLL_EXPORTS
#define RobotPathPlan_API extern "C" __declspec(dllexport)
#else
#define RobotPathPlan_API extern "C" __declspec(dllimport)
#endif

#include "PathPlanBasic.h"

RobotPathPlan_API int RobotPathPlan(int* &aPath, int &nLen);

RobotPathPlan_API int RobotReleaseArray();

RobotPathPlan_API int RobotLoadMap(char* sPath, int &nStartPx, int &nStartPy, int &nEndPx, int &nEndPy);

RobotPathPlan_API int RobotSaveMap(char* sPath);

RobotPathPlan_API int RobotCreateEmptyMap(int nWidth, int nHeight, int &nStartPx, int &nStartPy, int &nEndPx, int &nEndPy);

RobotPathPlan_API int RobotCreateRandomMap(int nWidth, int nHeight, int &nStartPx, int &nStartPy, int &nEndPx, int &nEndPy, float fDensity);

RobotPathPlan_API int setRobotPoint(int x, int y);

RobotPathPlan_API int setEndPoint(int x, int y);

RobotPathPlan_API int getMap(BYTE* &bMap);

RobotPathPlan_API int getWidth();

RobotPathPlan_API int getHeight();

#endif