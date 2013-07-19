using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace RobotPathPlanShow
{
    class DllAPI
    {
        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RobotPathPlan(ref IntPtr aPath, ref int nLen);

        public static int RobotPathPlan(ref int[] aPath, ref int nLen)
        {
            int Ret;
            IntPtr pPath = IntPtr.Zero;
            Ret = RobotPathPlan(ref pPath, ref nLen);
            if (Ret == 0)
            {
                aPath = new int[nLen];
                Marshal.Copy(pPath, aPath, 0, nLen);
            }
            return Ret;
        }

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RobotLoadMap(string sPath, ref int nStartPx, ref int nStartPy, ref int nEndPx, ref int nEndPy);

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RobotSaveMap(string sPath);

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RobotCreateEmptyMap(int nWidth, int nHeight, ref int nStartPx, ref int nStartPy, ref int nEndPx, ref int nEndPy);

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RobotCreateRandomMap(int nWidth, int nHeight, ref int nStartPx, ref int nStartPy, ref int nEndPx, ref int nEndPy, float fDensity);

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setRobotPoint(int x, int y);

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setEndPoint(int x, int y);

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getWidth();

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getHeight();

        [DllImport("RobotPathPlanDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getMap(ref IntPtr pMap);

        public static int getMap(ref byte[] bmap)
        {
            int nRet;
            IntPtr pMap = IntPtr.Zero;
            int nLen = DllAPI.getWidth() * DllAPI.getHeight();
            if (nLen <= 0)
            {
                return 1;
            }
            bmap = new byte[nLen];
            if ( (nRet = DllAPI.getMap(ref pMap)) != 0 )
            {
                return nRet;
            }
            Marshal.Copy(pMap, bmap, 0, nLen);
            return 0;
        }

        /// <summary>
        /// 只需要对Blank和Wall更新,Robot和EndPoint用SetRobotPoint/SetEndPoint就行了
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int UpdateMap(int x, int y, byte data)
        {
            IntPtr pMap = IntPtr.Zero;
            int _width = DllAPI.getWidth();
            DllAPI.getMap(ref pMap);
            Marshal.WriteByte(pMap, x + y * _width, data);
            return 0;
        }

        public static int ReadMapByte(int x, int y, ref byte data)
        {
            IntPtr pMap = IntPtr.Zero;
            int _width = DllAPI.getWidth();
            DllAPI.getMap(ref pMap);
            data = Marshal.ReadByte(pMap, x + y * _width);
            return 0;
        }

    }
}
