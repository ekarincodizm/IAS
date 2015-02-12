using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using IAS.DAL.Interfaces;
using IAS.DAL;

namespace IAS.DataServices.Test.Mocking
{
    //public class MockIASPersonEntities : IIASPersonEntities
    //{


    //    //#region Partial Methods

    //    //partial void OnContextCreated();

    //    //#endregion

    //    #region ObjectSet Properties

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_GL_HEADER> APPR_GL_HEADER
    //    {
    //        get
    //        {
    //            if ((_APPR_GL_HEADER == null))
    //            {
    //                _APPR_GL_HEADER = base.CreateObjectSet<APPR_GL_HEADER>("APPR_GL_HEADER");
    //            }
    //            return _APPR_GL_HEADER;
    //        }
    //    }
    //    private ObjectSet<APPR_GL_HEADER> _APPR_GL_HEADER;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_DO_ADDRESS> APPR_DO_ADDRESS
    //    {
    //        get
    //        {
    //            if ((_APPR_DO_ADDRESS == null))
    //            {
    //                _APPR_DO_ADDRESS = base.CreateObjectSet<APPR_DO_ADDRESS>("APPR_DO_ADDRESS");
    //            }
    //            return _APPR_DO_ADDRESS;
    //        }
    //    }
    //    private ObjectSet<APPR_DO_ADDRESS> _APPR_DO_ADDRESS;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_DO_HEADER> APPR_DO_HEADER
    //    {
    //        get
    //        {
    //            if ((_APPR_DO_HEADER == null))
    //            {
    //                _APPR_DO_HEADER = base.CreateObjectSet<APPR_DO_HEADER>("APPR_DO_HEADER");
    //            }
    //            return _APPR_DO_HEADER;
    //        }
    //    }
    //    private ObjectSet<APPR_DO_HEADER> _APPR_DO_HEADER;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_DO_REMARK> APPR_DO_REMARK
    //    {
    //        get
    //        {
    //            if ((_APPR_DO_REMARK == null))
    //            {
    //                _APPR_DO_REMARK = base.CreateObjectSet<APPR_DO_REMARK>("APPR_DO_REMARK");
    //            }
    //            return _APPR_DO_REMARK;
    //        }
    //    }
    //    private ObjectSet<APPR_DO_REMARK> _APPR_DO_REMARK;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_DO_DETAIL> APPR_DO_DETAIL
    //    {
    //        get
    //        {
    //            if ((_APPR_DO_DETAIL == null))
    //            {
    //                _APPR_DO_DETAIL = base.CreateObjectSet<APPR_DO_DETAIL>("APPR_DO_DETAIL");
    //            }
    //            return _APPR_DO_DETAIL;
    //        }
    //    }
    //    private ObjectSet<APPR_DO_DETAIL> _APPR_DO_DETAIL;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_GL_DETAIL> APPR_GL_DETAIL
    //    {
    //        get
    //        {
    //            if ((_APPR_GL_DETAIL == null))
    //            {
    //                _APPR_GL_DETAIL = base.CreateObjectSet<APPR_GL_DETAIL>("APPR_GL_DETAIL");
    //            }
    //            return _APPR_GL_DETAIL;
    //        }
    //    }
    //    private ObjectSet<APPR_GL_DETAIL> _APPR_GL_DETAIL;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_RECEIVE_DETAIL_D> APPR_RECEIVE_DETAIL_D
    //    {
    //        get
    //        {
    //            if ((_APPR_RECEIVE_DETAIL_D == null))
    //            {
    //                _APPR_RECEIVE_DETAIL_D = base.CreateObjectSet<APPR_RECEIVE_DETAIL_D>("APPR_RECEIVE_DETAIL_D");
    //            }
    //            return _APPR_RECEIVE_DETAIL_D;
    //        }
    //    }
    //    private ObjectSet<APPR_RECEIVE_DETAIL_D> _APPR_RECEIVE_DETAIL_D;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_RECEIVE_H> APPR_RECEIVE_H
    //    {
    //        get
    //        {
    //            if ((_APPR_RECEIVE_H == null))
    //            {
    //                _APPR_RECEIVE_H = base.CreateObjectSet<APPR_RECEIVE_H>("APPR_RECEIVE_H");
    //            }
    //            return _APPR_RECEIVE_H;
    //        }
    //    }
    //    private ObjectSet<APPR_RECEIVE_H> _APPR_RECEIVE_H;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPS_TABLE_YM_DOCNO> APPS_TABLE_YM_DOCNO
    //    {
    //        get
    //        {
    //            if ((_APPS_TABLE_YM_DOCNO == null))
    //            {
    //                _APPS_TABLE_YM_DOCNO = base.CreateObjectSet<APPS_TABLE_YM_DOCNO>("APPS_TABLE_YM_DOCNO");
    //            }
    //            return _APPS_TABLE_YM_DOCNO;
    //        }
    //    }
    //    private ObjectSet<APPS_TABLE_YM_DOCNO> _APPS_TABLE_YM_DOCNO;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPS_CONFIG_INPUT> APPS_CONFIG_INPUT
    //    {
    //        get
    //        {
    //            if ((_APPS_CONFIG_INPUT == null))
    //            {
    //                _APPS_CONFIG_INPUT = base.CreateObjectSet<APPS_CONFIG_INPUT>("APPS_CONFIG_INPUT");
    //            }
    //            return _APPS_CONFIG_INPUT;
    //        }
    //    }
    //    private ObjectSet<APPS_CONFIG_INPUT> _APPS_CONFIG_INPUT;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPS_SLC_USERS> APPS_SLC_USERS
    //    {
    //        get
    //        {
    //            if ((_APPS_SLC_USERS == null))
    //            {
    //                _APPS_SLC_USERS = base.CreateObjectSet<APPS_SLC_USERS>("APPS_SLC_USERS");
    //            }
    //            return _APPS_SLC_USERS;
    //        }
    //    }
    //    private ObjectSet<APPS_SLC_USERS> _APPS_SLC_USERS;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPM_ACCOUNT_CODE> APPM_ACCOUNT_CODE
    //    {
    //        get
    //        {
    //            if ((_APPM_ACCOUNT_CODE == null))
    //            {
    //                _APPM_ACCOUNT_CODE = base.CreateObjectSet<APPM_ACCOUNT_CODE>("APPM_ACCOUNT_CODE");
    //            }
    //            return _APPM_ACCOUNT_CODE;
    //        }
    //    }
    //    private ObjectSet<APPM_ACCOUNT_CODE> _APPM_ACCOUNT_CODE;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPM_PRODUCT> APPM_PRODUCT
    //    {
    //        get
    //        {
    //            if ((_APPM_PRODUCT == null))
    //            {
    //                _APPM_PRODUCT = base.CreateObjectSet<APPM_PRODUCT>("APPM_PRODUCT");
    //            }
    //            return _APPM_PRODUCT;
    //        }
    //    }
    //    private ObjectSet<APPM_PRODUCT> _APPM_PRODUCT;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPS_TABLE_DOCUMENT> APPS_TABLE_DOCUMENT
    //    {
    //        get
    //        {
    //            if ((_APPS_TABLE_DOCUMENT == null))
    //            {
    //                _APPS_TABLE_DOCUMENT = base.CreateObjectSet<APPS_TABLE_DOCUMENT>("APPS_TABLE_DOCUMENT");
    //            }
    //            return _APPS_TABLE_DOCUMENT;
    //        }
    //    }
    //    private ObjectSet<APPS_TABLE_DOCUMENT> _APPS_TABLE_DOCUMENT;

    //    /// <summary>
    //    /// No Metadata Documentation available.
    //    /// </summary>
    //    public ObjectSet<APPR_BD_HEADER> APPR_BD_HEADER
    //    {
    //        get
    //        {
    //            if ((_APPR_BD_HEADER == null))
    //            {
    //                _APPR_BD_HEADER = base.CreateObjectSet<APPR_BD_HEADER>("APPR_BD_HEADER");
    //            }
    //            return _APPR_BD_HEADER;
    //        }
    //    }
    //    private ObjectSet<APPR_BD_HEADER> _APPR_BD_HEADER;

    //    #endregion

    //    #region AddTo Methods

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_GL_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_GL_HEADER(APPR_GL_HEADER aPPR_GL_HEADER)
    //    {
    //        base.AddObject("APPR_GL_HEADER", aPPR_GL_HEADER);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_DO_ADDRESS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_DO_ADDRESS(APPR_DO_ADDRESS aPPR_DO_ADDRESS)
    //    {
    //        base.AddObject("APPR_DO_ADDRESS", aPPR_DO_ADDRESS);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_DO_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_DO_HEADER(APPR_DO_HEADER aPPR_DO_HEADER)
    //    {
    //        base.AddObject("APPR_DO_HEADER", aPPR_DO_HEADER);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_DO_REMARK EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_DO_REMARK(APPR_DO_REMARK aPPR_DO_REMARK)
    //    {
    //        base.AddObject("APPR_DO_REMARK", aPPR_DO_REMARK);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_DO_DETAIL EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_DO_DETAIL(APPR_DO_DETAIL aPPR_DO_DETAIL)
    //    {
    //        base.AddObject("APPR_DO_DETAIL", aPPR_DO_DETAIL);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_GL_DETAIL EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_GL_DETAIL(APPR_GL_DETAIL aPPR_GL_DETAIL)
    //    {
    //        base.AddObject("APPR_GL_DETAIL", aPPR_GL_DETAIL);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_RECEIVE_DETAIL_D EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_RECEIVE_DETAIL_D(APPR_RECEIVE_DETAIL_D aPPR_RECEIVE_DETAIL_D)
    //    {
    //        base.AddObject("APPR_RECEIVE_DETAIL_D", aPPR_RECEIVE_DETAIL_D);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_RECEIVE_H EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_RECEIVE_H(APPR_RECEIVE_H aPPR_RECEIVE_H)
    //    {
    //        base.AddObject("APPR_RECEIVE_H", aPPR_RECEIVE_H);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPS_TABLE_YM_DOCNO EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPS_TABLE_YM_DOCNO(APPS_TABLE_YM_DOCNO aPPS_TABLE_YM_DOCNO)
    //    {
    //        base.AddObject("APPS_TABLE_YM_DOCNO", aPPS_TABLE_YM_DOCNO);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPS_CONFIG_INPUT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPS_CONFIG_INPUT(APPS_CONFIG_INPUT aPPS_CONFIG_INPUT)
    //    {
    //        base.AddObject("APPS_CONFIG_INPUT", aPPS_CONFIG_INPUT);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPS_SLC_USERS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPS_SLC_USERS(APPS_SLC_USERS aPPS_SLC_USERS)
    //    {
    //        base.AddObject("APPS_SLC_USERS", aPPS_SLC_USERS);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPM_ACCOUNT_CODE EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPM_ACCOUNT_CODE(APPM_ACCOUNT_CODE aPPM_ACCOUNT_CODE)
    //    {
    //        base.AddObject("APPM_ACCOUNT_CODE", aPPM_ACCOUNT_CODE);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPM_PRODUCT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPM_PRODUCT(APPM_PRODUCT aPPM_PRODUCT)
    //    {
    //        base.AddObject("APPM_PRODUCT", aPPM_PRODUCT);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPS_TABLE_DOCUMENT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPS_TABLE_DOCUMENT(APPS_TABLE_DOCUMENT aPPS_TABLE_DOCUMENT)
    //    {
    //        base.AddObject("APPS_TABLE_DOCUMENT", aPPS_TABLE_DOCUMENT);
    //    }

    //    /// <summary>
    //    /// Deprecated Method for adding a new object to the APPR_BD_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
    //    /// </summary>
    //    public void AddToAPPR_BD_HEADER(APPR_BD_HEADER aPPR_BD_HEADER)
    //    {
    //        base.AddObject("APPR_BD_HEADER", aPPR_BD_HEADER);
    //    }

    //    #endregion
    //}
}
