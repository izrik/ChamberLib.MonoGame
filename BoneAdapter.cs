﻿using System;
using XBone = Microsoft.Xna.Framework.Graphics.ModelBone;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public class BoneAdapter : IBone
    {
        protected static readonly Dictionary<XBone, IBone> _cache = new Dictionary<XBone, IBone>();

        public static IBone GetAdapter(XBone bone)
        {
            if (_cache.ContainsKey(bone))
            {
                return _cache[bone];
            }

            var adapter = new BoneAdapter(bone);
            _cache[bone] = adapter;
            return adapter;
        }

        protected BoneAdapter(XBone bone)
        {
            Bone = bone;
        }

        public XBone Bone;

        public string Name
        {
            get { return Bone.Name; }
            set { Bone.Name = value; }
        }

        public Matrix Transform
        {
            get { return Bone.Transform.ToChamber(); }
            set { Bone.Transform = value.ToXna(); }
        }

        public int Index
        {
            get { return Bone.Index; }
            set { Bone.Index = value; }
        }

        public IBone Parent
        {
            get
            {
                return GetAdapter(Bone.Parent);
            }
            set
            {
                Bone = ((BoneAdapter)value).Bone;
            }
        }

        public List<IBone> Children
        {
            get
            {
                return Bone.Children.Select(x => GetAdapter(x)).ToList();
            }
        }
    }
}

