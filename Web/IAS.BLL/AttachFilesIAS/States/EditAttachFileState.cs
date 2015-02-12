using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS.States
{
    public class EditAttachFileState : AttachFileState
    {
        public EditAttachFileState()
        {
            base.Id = Status.Value();
        }
        public override AttachFileStatus Status
        {
            get { return AttachFileStatus.Edit; }
        }

        public override void Submit(AttachFile attachFile)
        {
            throw new NotImplementedException();
        }
    }
}
