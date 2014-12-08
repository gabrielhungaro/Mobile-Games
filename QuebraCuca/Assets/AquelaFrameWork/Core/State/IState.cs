using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;

namespace AquelaFrameWork.Core.State
{
    public interface IState
    {
        /// <summary>
        /// 
        /// Initialize the State
        /// 
        /// </summary>
        void Initialize();

        void BuildState();

        void Destroy();
        void Pause();
        void Resume();
        void AFUpdate(double deltaTime);

        string GetName();
        AState.EGameState GetStateID();

        int GetID();


        bool IsDestroyable();
        void SetDestroyable(bool value);

		AFObject Add( AFObject obj );
		Entity AddEntity( Entity entity, object view = null);
		void Remove( AFObject obj );
		AFObject GetObjectByName( string name );
        T GetFirstObjectByType<T>() where T : AFObject;
        List<T> GetObjectsByType<T>(Type type) where T : AFObject;
    }
}
