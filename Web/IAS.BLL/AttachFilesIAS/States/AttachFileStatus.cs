using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.Properties;

namespace IAS.BLL.AttachFilesIAS.States
{
    public enum AttachFileStatus
    {
        Active=1,
        Edit=2,
        Wait=3,
        Delete=4,
    }

    public static class AttachFileValue
    {
        public static String Value(this AttachFileStatus attachFileStatus)
        {

            switch (attachFileStatus)
            {
                case AttachFileStatus.Active:
                    return "A";

                case AttachFileStatus.Edit:
                    return "E";

                case AttachFileStatus.Wait:
                    return "W";

                case AttachFileStatus.Delete:
                    return "D";

                default:
                    throw new InvalidOperationException(Resources.errorAttachFileStatus_001);

            }


        }
    }
}
