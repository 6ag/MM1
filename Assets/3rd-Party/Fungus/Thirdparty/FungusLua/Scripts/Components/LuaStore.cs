// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using MoonSharp.Interpreter;
using System.Collections.Generic;

namespace Fungus
{
    /// <summary>
    /// Wrapper for a prime Lua table that persists across scene loads. 
    /// This is useful for transferring values from one scene to another. One one LuaStore component may exist 
    /// in a scene at a time.
    /// </summary>
    public class LuaStore : LuaBindingsBase
    {
        protected Table primeTable;

        protected bool initialized;

        protected static LuaStore instance;

        protected virtual void Start()
        {
            Init();
        }

        /// <summary>
        /// Initialize the LuaStore component.
        /// This component behaves somewhat like a singleton in that only one instance
        /// is permitted in the application which persists until shutdown.
        /// </summary>
        protected virtual bool Init()
        {
            if (initialized)    
            {
                return true;
            }

            if (instance == null)
            {
                // This is the first instance of the LuaStore, so store a static reference to it.
                instance = this;
            }
            else if (instance != this)
            {
                // This is an extra instance of LuaStore. We only need one in the scene, so delete this one.
                Destroy(gameObject);
                return false;
            }

            // We're now guaranteed that this instance of LuaStore is the first and only instance.

            primeTable = DynValue.NewPrimeTable().Table;

            // DontDestroyOnLoad only works for root objects
            transform.parent = null;

            DontDestroyOnLoad(this);

            initialized = true;

            return true;
        }

        #region Public members

        /// <summary>
        /// A Lua table that can be shared between multiple LuaEnvironments.
        /// </summary>
        public virtual Table PrimeTable { get { return primeTable; } }

        #endregion

        #region LuaBindingsBase implementation

        public override void AddBindings(LuaEnvironment luaEnv)
        {
            if (!Init())
            {
                return;
            }

            MoonSharp.Interpreter.Script interpreter = luaEnv.Interpreter;
            Table globals = interpreter.Globals;

            if (globals == null)
            {
                Debug.LogError("Lua globals table is null");
                return;
            }

            // If the fungus global table is defined then add the store to it
            Table fungusTable = globals.Get("fungus").Table;
            if (fungusTable != null)
            {
                fungusTable["store"] = primeTable;
            }
            else
            {
                // Add the store as a global
                globals["store"] = primeTable;
            }
        }

        public override List<BoundObject> BoundObjects { get { return null; } }

        #endregion
    }
}