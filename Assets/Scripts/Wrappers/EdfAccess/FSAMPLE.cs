﻿using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using SREYELINKLib;

//the arrays may need to be set as unsafe | fixed float gxvel[3];
namespace Eyelink.Structs {
    public unsafe struct FSAMPLE {
        public uint time;
        public fixed float px[2];
        public fixed float py[2];
        public fixed float hx[2];
        public fixed float hy[2];
        public fixed float pa[2];
        public fixed float gx[2];
        public fixed float gy[2];
        public float rx;
        public float ry;
        public fixed float gxvel[2];
        public fixed float gyvel[2];
        public fixed float hxvel[2];
        public fixed float hyvel[2];
        public fixed float rxvel[2];
        public fixed float ryvel[2];
        public fixed float fgxvel[2];
        public fixed float fgyvel[2];
        public fixed float fhxvel[2];
        public fixed float fhyvel[2];
        public fixed float frxvel[2];
        public fixed float fryvel[2];
        public fixed short hdata[8];

        //flags to indicate contents
        public ushort flags;

        public ushort input;
        public ushort buttons;
        public short htype;
        public ushort errors;

        public unsafe Vector2 RightGaze {
            get {
                fixed (float* ptr = gx, ptr2 = gy) {
                    return new Vector2(*ptr, *ptr2);
                }
            }
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder(base.ToString());

            fixed (float* x = hx) {
                float[] arr = ToArray(fixedArr: x, len: 2);
            }

            return builder.ToString();
        }

        private static float[] ToArray(float* fixedArr, int len) {
            float[] arr = new float[len];

            for (int i = 0; i < len; i++) {
                arr[i] = *(fixedArr + i);
            }

            return arr;
        }
    }

    public class Fsample : AllFloatData {
        public readonly Vector2 rightGaze;

        public Fsample(FSAMPLE sample, DataTypes datatype) : base(datatype, sample.time) {
            rightGaze = sample.RightGaze;
        }

        public Fsample(uint time, float gx, float gy, DataTypes datatype) : base(datatype, time) {
            rightGaze = new Vector2(gx, gy);
        }

        public override string ToString() {
            return $"{dataType} @ {time} | {rightGaze}";
        }
    }
}
