﻿update doc."Files" f set "IsFileReadonly" = 't' where exists (select * from doc."StaticFiles" s where s."ID" = f."ID")