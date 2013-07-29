
#ifndef PATHPLAN_H
#define PATHPLAN_H

#include "PathPlanBasic.h"

int FindPath(point &startP, point &endP, int *&aPath, int &nLen);

int ReleasePathArray();

int InitializePathPlan(int nWidth, int nHeight);

int UnInitializePathPlan();

#endif