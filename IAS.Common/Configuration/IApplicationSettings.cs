using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Configuration
{
    public interface IApplicationSettings
    {
        String SSOAuth { get; }

        String LoggerName { get; }

        String WebPublicUrl { get; } //" value="http://localhost:15555/soria/"/>
        String ADPath { get; } //" value="LDAP://192.168.110.91"/>-->
        String ADUserName { get; } //" value="artest"/>
        String ADPassword { get; } //" value="artest123"/>
        String ADDomain { get; } //" value="oic.or.th"/>

        String FS_TEMP { get; } //" value="Temp"/>
        String FS_ATTACH { get; } //" value="AttachFile"/>
        String FS_OIC { get; } //" value="OIC"/>
        String FS_RECEIVE { get; } //" value="ReceiptFile"/>
        String PAGE_SIZE { get; } //" value="10"/>

        String OIC_SECTION { get; } //" value="12122" />
        String OIC_BRANCH_NO { get; } //" value="1" />
        String EmailOut { get; } //" value="amsadmin@oic.or.th"/>
        String EmailOutPass { get; } //" value="gvgvH,gvl09"/>


        String TEMP_FOLDER_ATTACH { get; } //" value="Temp"/>

        String OIC_FOLDER_ATTACH { get; } //" value="OIC"/>

        String FOLDER_ATTACH { get; } //" value="AttachFile"/>

        String COMPRESS_FOLDER { get; } //" value="CompressLicense"/>

        String CODE_ATTACH_PHOTO { get; } //" value="04"/>

        String DEFAULT_NET_DRIVE { get; } //" value="\\192.168.16.24\IASFileUpload\"/>
        String USER_NET_DRIVE { get; } //" value="administrator"/>
        String PASS_NET_DRIVE { get; } //" value="p@ssw0rd"/>

        String CITYBANK_ACCOUNT { get; } //" value="11010201010910"/>
        String CITYBANK_GROUP { get; } //" value="IV062"/>

        String KTB_ACCOUNT { get; } //" value="11010201010200"/>
        String KTB_GROUP { get; } //" value="IV005"/>



    }
}
