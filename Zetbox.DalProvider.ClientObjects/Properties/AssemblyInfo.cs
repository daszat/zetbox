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
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The Common Language Specification (CLS) defines naming restrictions, data types,
// and rules to which assemblies must conform if they are to be used across programming
// languages. This assembly should be compatible even if it is probably not useful.
[assembly: CLSCompliant(true)]

// allow tests to reach into internal parts of us
[assembly: InternalsVisibleTo("Zetbox.DalProvider.ClientObjects.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100a78ef02c99b017c1d2527b579ab3c43b2ba2e8e1ce6464855d987944fe2e2e465c42eee0b9bb2c07284184cd85f3f7ebbb3057fb51c950e4cd4d88ecd74772f356e8c6da44b97e09613587560ee884d885075ad9ec102a95d52914cecfbb00a4b30a55fe2301b7c748338669650fc02dd9793c922bff0e8344433949e6d1e2e2")]
