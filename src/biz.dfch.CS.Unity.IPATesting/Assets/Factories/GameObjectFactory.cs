/**
 * Copyright 2020 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using Assets.Models;
using UnityEngine;

namespace Assets.Factories
{
    public abstract class GameObjectFactory<T> where T : IGameObjectInfo, new()
    {
        public abstract List<GameObject> CreateMany(List<T> gameObjectInfos);
        public abstract GameObject Create(T gameObjectInfo);
    }

    //public interface IGameObjectFactory
    //{
    //    List<GameObject> CreateMany(List<IGameObjectInfo> gameObjectInfos);
    //    GameObject Create(IGameObjectInfo gameObjectInfo);
    //}
    //
    //public class CubeFactory2 : IGameObjectFactory
    //{
    //    public List<GameObject> CreateMany(List<IGameObjectInfo> gameObjectInfos)
    //    {
    //        if (gameObjectInfos.All(g => g.GetType() == typeof(CubeInfo)))
    //        {
    //            throw new System.NotImplementedException();
    //        }
    //        throw new System.NotImplementedException();
    //    }
    //
    //    public GameObject Create(IGameObjectInfo gameObjectInfo)
    //    {
    //        if (gameObjectInfo.GetType() == typeof(CubeInfo))
    //        {
    //            throw new System.NotImplementedException();
    //        }
    //        throw new System.NotImplementedException();
    //    }
    //}
    //
    //public abstract class GameObjectFactory2
    //{
    //    protected abstract List<GameObject> CreateMany(List<IFirst> gameObjectInfos);
    //    public abstract GameObject Create(IGameObjectInfo gameInfo);
    //}
    //
    //public interface IFirst
    //{
    //
    //}
    //
    //public class Second : IFirst
    //{
    //
    //}
    //
    //public class XFactory : GameObjectFactory2
    //{
    //    protected override List<GameObject> CreateMany(List<IFirst> gameObjectInfos)
    //    {
    //        if (gameObjectInfos.All(g => g.GetType() == typeof(Second)))
    //        {
    //            throw new System.NotImplementedException();
    //        }
    //        throw new System.NotImplementedException();
    //    }
    //
    //    public override GameObject Create(IGameObjectInfo gameInfo)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //public interface IHouse
    //{
    //    int GetAmountOfDoors();
    //}
    //
    //public abstract class AbstractHouse
    //{
    //    protected AbstractHouse(int i)
    //    {
    //    }
    //
    //    public abstract int GetAmountOfDoors();
    //
    //    protected void Create()
    //    {
    //        // Standardverhalten
    //    }
    //}
    //
    //public class MyHouse : AbstractHouse
    //{
    //    public MyHouse(int i) : base(i)
    //    {
    //
    //    }
    //
    //    public override int GetAmountOfDoors()
    //    {
    //        Create();
    //        return 0;
    //    }
    //
    //}
}
