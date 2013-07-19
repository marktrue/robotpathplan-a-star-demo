
#ifndef PATHPLANBASIC_H
#define PATHPLANBASIC_H

typedef unsigned char BYTE;

enum RET_VAL
{
	RPP_SUCCESS = 0,
	RPP_FAILED = 1,
	RPP_FILEOPENFAILED = 2
};

enum PointType
{
	PntType_Robot = 's',
	PntType_Goal = 'g',
	PntType_Normal = 0,
	PntType_Wall = 'o'
};

struct point
{
	int x;
	int y;
};

extern "C" int g_nWidth;
extern "C" int g_nHeight;
extern "C" BYTE* g_bMap;
extern "C" point g_pntStart;
extern "C" point g_pntEnd;

#endif
