using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.AttachFilesIAS.States
{
    public interface IAttachFileState
    {
        String Id { get; set; }
        AttachFileStatus Status { get; }

        void Submit(AttachFile attachFile);
    }
}
