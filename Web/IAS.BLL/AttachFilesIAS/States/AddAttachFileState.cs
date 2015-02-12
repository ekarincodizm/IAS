using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS.States
{
    public class ActiveAttachFileState : AttachFileState
    {
        public ActiveAttachFileState()
        {
            Id = Status.Value();
        }
        public override AttachFileStatus Status
        {
            get { return AttachFileStatus.Active; }
        }

        public override void Submit(AttachFile attachFile)
        {
            throw new NotImplementedException();
        }
    }
}
