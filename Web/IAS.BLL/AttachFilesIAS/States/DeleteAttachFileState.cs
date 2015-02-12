using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.Properties;

namespace IAS.BLL.AttachFilesIAS.States
{
    public class DeleteAttachFileState : AttachFileState
    {
        public DeleteAttachFileState()
        {
            base.Id = Status.Value();
        }
        public override AttachFileStatus Status
        {
            get { return AttachFileStatus.Delete; }
        }

        public override void Submit(AttachFile attachFile)
        {
            throw new InvalidOperationException(Resources.errorDeleteAttachFileState_001);
        }
    }
}
