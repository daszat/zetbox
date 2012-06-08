using System;
using Kistl.API;

// This shows the current branch, number of commits since the epoch, commit hash and whether(1) or not(0) the working directory is dirty
[assembly: KistlSourceRevision("$BRANCH$-$REVNUM$-$REVID$-$DIRTY$")]
