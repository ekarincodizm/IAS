using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS.States
{
    public abstract class AttachFileState : IAttachFileState
    {
        public String Id { get; set; }

        public abstract AttachFileStatus Status { get; }

        public abstract void Submit(AttachFile attachFile);
    }
}
