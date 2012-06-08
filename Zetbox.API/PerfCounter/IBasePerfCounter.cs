// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.API.PerfCounter
{
    using System;

    public interface IBasePerfCounter
    {
        long IncrementQuery(Zetbox.API.InterfaceType ifType);
        void DecrementQuery(Zetbox.API.InterfaceType ifType, int objectCount, long startTicks);

        long IncrementSubmitChanges();
        void DecrementSubmitChanges(int objectCount, long startTicks);

        long IncrementGetList(Zetbox.API.InterfaceType ifType);
        void DecrementGetList(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementGetListOf(Zetbox.API.InterfaceType ifType);
        void DecrementGetListOf(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementFetchRelation(Zetbox.API.InterfaceType ifType);
        void DecrementFetchRelation(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementSetObjects();
        void DecrementSetObjects(int objectCount, long startTicks);

        void IncrementServerMethodInvocation();
        
        void Initialize(Zetbox.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump();
    }

    public interface IBasePerfCounterAppender
    {
        void IncrementFetchRelation(Zetbox.API.InterfaceType ifType);
        void DecrementFetchRelation(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks, long endTicks);

        void IncrementGetList(Zetbox.API.InterfaceType ifType);
        void DecrementGetList(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks, long endTicks);

        void IncrementGetListOf(Zetbox.API.InterfaceType ifType);
        void DecrementGetListOf(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks, long endTicks);

        void IncrementQuery(Zetbox.API.InterfaceType ifType);
        void DecrementQuery(Zetbox.API.InterfaceType ifType, int objectCount, long startTicks, long endTicks);

        void IncrementSetObjects();
        void DecrementSetObjects(int objectCount, long startTicks, long endTicks);

        void IncrementSubmitChanges();
        void DecrementSubmitChanges(int objectCount, long startTicks, long endTicks);

        void IncrementServerMethodInvocation();
        
        void Initialize(Zetbox.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump(bool force);
    }
}
