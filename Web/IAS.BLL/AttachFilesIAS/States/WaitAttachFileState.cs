using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS.States
{
    public class WaitAttachFileState : AttachFileState
    {
        public WaitAttachFileState()
        {
            base.Id = Status.Value();
        }
        public override AttachFileStatus Status
        {
            get { return AttachFileStatus.Wait; }
        }

        public override void Submit(AttachFile attachFile)
        {
            throw new NotImplementedException();
        }
    }
}
