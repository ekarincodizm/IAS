﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using System.Runtime;
using System.Collections;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;

namespace IAS.DAL.Interfaces
{
    public interface IIASFinanceEntities
    {

    
        //#region Partial Methods
    
        //partial void OnContextCreated();
    
        //#endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_GL_HEADER> APPR_GL_HEADER { get; }

    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_DO_ADDRESS> APPR_DO_ADDRESS { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_DO_HEADER> APPR_DO_HEADER { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_DO_REMARK> APPR_DO_REMARK { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_DO_DETAIL> APPR_DO_DETAIL { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_GL_DETAIL> APPR_GL_DETAIL { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_RECEIVE_DETAIL_D> APPR_RECEIVE_DETAIL_D { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_RECEIVE_H> APPR_RECEIVE_H { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPS_TABLE_YM_DOCNO> APPS_TABLE_YM_DOCNO { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPS_CONFIG_INPUT> APPS_CONFIG_INPUT { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPS_SLC_USERS> APPS_SLC_USERS { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPM_ACCOUNT_CODE> APPM_ACCOUNT_CODE { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPM_PRODUCT> APPM_PRODUCT { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPS_TABLE_DOCUMENT> APPS_TABLE_DOCUMENT { get; }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        IObjectSet<APPR_BD_HEADER> APPR_BD_HEADER { get; }

        IObjectSet<APPM_BUDGET_CODE> APPM_BUDGET_CODE { get; }

        IObjectSet<APPM_RECEIVE_GROUP> APPM_RECEIVE_GROUP { get; }
        #endregion

        #region AddTo Methods                     
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_GL_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_GL_HEADER(APPR_GL_HEADER aPPR_GL_HEADER);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_DO_ADDRESS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_DO_ADDRESS(APPR_DO_ADDRESS aPPR_DO_ADDRESS);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_DO_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_DO_HEADER(APPR_DO_HEADER aPPR_DO_HEADER);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_DO_REMARK EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_DO_REMARK(APPR_DO_REMARK aPPR_DO_REMARK);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_DO_DETAIL EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_DO_DETAIL(APPR_DO_DETAIL aPPR_DO_DETAIL);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_GL_DETAIL EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_GL_DETAIL(APPR_GL_DETAIL aPPR_GL_DETAIL);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_RECEIVE_DETAIL_D EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_RECEIVE_DETAIL_D(APPR_RECEIVE_DETAIL_D aPPR_RECEIVE_DETAIL_D);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_RECEIVE_H EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_RECEIVE_H(APPR_RECEIVE_H aPPR_RECEIVE_H);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPS_TABLE_YM_DOCNO EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPS_TABLE_YM_DOCNO(APPS_TABLE_YM_DOCNO aPPS_TABLE_YM_DOCNO);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPS_CONFIG_INPUT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPS_CONFIG_INPUT(APPS_CONFIG_INPUT aPPS_CONFIG_INPUT);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPS_SLC_USERS EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPS_SLC_USERS(APPS_SLC_USERS aPPS_SLC_USERS);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPM_ACCOUNT_CODE EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPM_ACCOUNT_CODE(APPM_ACCOUNT_CODE aPPM_ACCOUNT_CODE);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPM_PRODUCT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPM_PRODUCT(APPM_PRODUCT aPPM_PRODUCT);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPS_TABLE_DOCUMENT EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPS_TABLE_DOCUMENT(APPS_TABLE_DOCUMENT aPPS_TABLE_DOCUMENT);
    
        /// <summary>
        /// Deprecated Method for adding a new object to the APPR_BD_HEADER EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        void AddToAPPR_BD_HEADER(APPR_BD_HEADER aPPR_BD_HEADER);


        void AddToAPPM_BUDGET_CODE(APPM_BUDGET_CODE aAPPM_BUDGET_CODE);

        void AddToAPPM_RECEIVE_GROUP(APPM_RECEIVE_GROUP aAPPM_RECEIVE_GROUP);
        #endregion


        #region ObjectContext

        int? CommandTimeout { get; set; }
        //
        // Summary:
        //     Gets the connection used by the object context.
        //
        // Returns:
        //     A System.Data.Common.DbConnection object that is the connection.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     When the System.Data.Objects.ObjectContext instance has been disposed.
        DbConnection Connection { get; }
        //
        // Summary:
        //     Gets the System.Data.Objects.ObjectContextOptions instance that contains
        //     options that affect the behavior of the System.Data.Objects.ObjectContext.
        //
        // Returns:
        //     The System.Data.Objects.ObjectContextOptions instance that contains options
        //     that affect the behavior of the System.Data.Objects.ObjectContext.
        ObjectContextOptions ContextOptions { get; }
        //
        // Summary:
        //     Gets or sets the default container name.
        //
        // Returns:
        //     A System.String that is the default container name.
        string DefaultContainerName { get; set; }
        //
        // Summary:
        //     Gets the metadata workspace used by the object context.
        //
        // Returns:
        //     The System.Data.Metadata.Edm.MetadataWorkspace object associated with this
        //     System.Data.Objects.ObjectContext.
        [CLSCompliant(false)]
        MetadataWorkspace MetadataWorkspace { get; }
        //
        // Summary:
        //     Gets the object state manager used by the object context to track object
        //     changes.
        //
        // Returns:
        //     The System.Data.Objects.ObjectStateManager used by this System.Data.Objects.ObjectContext.
        ObjectStateManager ObjectStateManager { get; }
        //
        // Summary:
        //     Gets the LINQ query provider associated with this object context.
        //
        // Returns:
        //     The System.Linq.IQueryProvider instance used by this object context.

        // Summary:
        //     Occurs when a new entity object is created from data in the data source as
        //     part of a query or load operation.
        event ObjectMaterializedEventHandler ObjectMaterialized;
        //
        // Summary:
        //     Occurs when changes are saved to the data source.
        event EventHandler SavingChanges;

        // Summary:
        //     Accepts all changes made to objects in the object context.
        void AcceptAllChanges();
        //
        // Summary:
        //     Adds an object to the object context.
        //
        // Parameters:
        //   entitySetName:
        //     Represents the entity set name, which may optionally be qualified by the
        //     entity container name.
        //
        //   entity:
        //     The System.Object to add.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The entity parameter is null. -or-The entitySetName does not qualify.
        void AddObject(string entitySetName, object entity);
        //
        // Summary:
        //     Copies the scalar values from the supplied object into the object in the
        //     System.Data.Objects.ObjectContext that has the same key.
        //
        // Parameters:
        //   entitySetName:
        //     The name of the entity set to which the object belongs.
        //
        //   currentEntity:
        //     The detached object that has property updates to apply to the original object.
        //     The entity key of currentEntity must match the System.Data.Objects.ObjectStateEntry.EntityKey
        //     property of an entry in the System.Data.Objects.ObjectContext.
        //
        // Type parameters:
        //   TEntity:
        //     The entity type of the object.
        //
        // Returns:
        //     The updated object.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     entitySetName or current is null.
        //
        //   System.InvalidOperationException:
        //     The System.Data.Metadata.Edm.EntitySet from entitySetName does not match
        //     the System.Data.Metadata.Edm.EntitySet of the object’s System.Data.EntityKey.-or-The
        //     object is not in the System.Data.Objects.ObjectStateManager or it is in a
        //     System.Data.EntityState.Detached state.-or- The entity key of the supplied
        //     object is invalid.
        //
        //   System.ArgumentException:
        //     entitySetName is an empty string.
        TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class;
        //
        // Summary:
        //     Copies the scalar values from the supplied object into set of original values
        //     for the object in the System.Data.Objects.ObjectContext that has the same
        //     key.
        //
        // Parameters:
        //   entitySetName:
        //     The name of the entity set to which the object belongs.
        //
        //   originalEntity:
        //     The detached object that has original values to apply to the object. The
        //     entity key of originalEntity must match the System.Data.Objects.ObjectStateEntry.EntityKey
        //     property of an entry in the System.Data.Objects.ObjectContext.
        //
        // Type parameters:
        //   TEntity:
        //     The type of the entity object.
        //
        // Returns:
        //     The updated object.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     entitySetName or original is null.
        //
        //   System.InvalidOperationException:
        //     The System.Data.Metadata.Edm.EntitySet from entitySetName does not match
        //     the System.Data.Metadata.Edm.EntitySet of the object’s System.Data.EntityKey.-or-An
        //     System.Data.Objects.ObjectStateEntry for the object cannot be found in the
        //     System.Data.Objects.ObjectStateManager. -or-The object is in an System.Data.EntityState.Added
        //     or a System.Data.EntityState.Detached state.-or- The entity key of the supplied
        //     object is invalid or has property changes.
        //
        //   System.ArgumentException:
        //     entitySetName is an empty string.
        TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class;
        //
        // Summary:
        //     Applies property changes from a detached object to an object already attached
        //     to the object context.
        //
        // Parameters:
        //   entitySetName:
        //     The name of the entity set to which the object belongs.
        //
        //   changed:
        //     The detached object that has property updates to apply to the original object.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     When entitySetName is null or an empty string.-or-When changed is null.
        //
        //   System.InvalidOperationException:
        //     When the System.Data.Metadata.Edm.EntitySet from entitySetName does not match
        //     the System.Data.Metadata.Edm.EntitySet of the object’s System.Data.EntityKey.-or-When
        //     the entity is in a state other than System.Data.EntityState.Modified or System.Data.EntityState.Unchanged.-or-
        //     The original object is not attached to the context.
        //
        //   System.ArgumentException:
        //     When the type of the changed object is not the same type as the original
        //     object.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use ApplyCurrentValues instead")]
        [Browsable(false)]
        void ApplyPropertyChanges(string entitySetName, object changed);
        //
        // Summary:
        //     Attaches an object or object graph to the object context when the object
        //     has an entity key.
        //
        // Parameters:
        //   entity:
        //     The object to attach.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The entity is null.
        //
        //   System.InvalidOperationException:
        //     Invalid entity key.
        void Attach(IEntityWithKey entity);
        //
        // Summary:
        //     Attaches an object or object graph to the object context in a specific entity
        //     set.
        //
        // Parameters:
        //   entitySetName:
        //     Represents the entity set name, which may optionally be qualified by the
        //     entity container name.
        //
        //   entity:
        //     The System.Object to attach.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The entity is null.
        //
        //   System.InvalidOperationException:
        //     Invalid entity set.-or-The object has a temporary key. -or-The object has
        //     an System.Data.EntityKey and the System.Data.Metadata.Edm.EntitySet does
        //     not match with the entity set passed in as an argument of the method.-or-The
        //     object does not have an System.Data.EntityKey and no entity set is provided.-or-Any
        //     object from the object graph has a temporary System.Data.EntityKey.-or-Any
        //     object from the object graph has an invalid System.Data.EntityKey (for example,
        //     values in the key do not match values in the object).-or-The entity set could
        //     not be found from a given entitySetName name and entity container name.-or-Any
        //     object from the object graph already exists in another state manager.
        void AttachTo(string entitySetName, object entity);
        //
        // Summary:
        //     Creates the database by using the current data source connection and the
        //     metadata in the System.Data.Metadata.Edm.StoreItemCollection.
        void CreateDatabase();
        //
        // Summary:
        //     Generates a data definition language (DDL) script that creates schema objects
        //     (tables, primary keys, foreign keys) for the metadata in the System.Data.Metadata.Edm.StoreItemCollection.
        //     The System.Data.Metadata.Edm.StoreItemCollection loads metadata from store
        //     schema definition language (SSDL) files.
        //
        // Returns:
        //     A DDL script that creates schema objects for the metadata in the System.Data.Metadata.Edm.StoreItemCollection.
        string CreateDatabaseScript();
        //
        // Summary:
        //     Creates the entity key for a specific object, or returns the entity key if
        //     it already exists.
        //
        // Parameters:
        //   entitySetName:
        //     The fully qualified name of the entity set to which the entity object belongs.
        //
        //   entity:
        //     The object for which the entity key is being retrieved.
        //
        // Returns:
        //     The System.Data.EntityKey of the object.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     When either parameter is null.
        //
        //   System.ArgumentException:
        //     When entitySetName is empty.-or- When the type of the entity object does
        //     not exist in the entity set.-or-When the entitySetName is not fully qualified.
        //
        //   System.InvalidOperationException:
        //     When the entity key cannot be constructed successfully based on the supplied
        //     parameters.
        EntityKey CreateEntityKey(string entitySetName, object entity);
        //
        // Summary:
        //     Creates and returns an instance of the requested type .
        //
        // Type parameters:
        //   T:
        //     Type of object to be returned.
        //
        // Returns:
        //     An instance of the requested type T, or an instance of a derived type that
        //     enables T to be used with the Entity Framework. The returned object is either
        //     an instance of the requested type or an instance of a derived type that enables
        //     the requested type to be used with the Entity Framework.
        T CreateObject<T>() where T : class;
        //
        // Summary:
        //     Creates a new System.Data.Objects.IIObjectSet<TEntity> instance that is used
        //     to query, add, modify, and delete objects of the specified entity type.
        //
        // Type parameters:
        //   TEntity:
        //     Entity type of the requested System.Data.Objects.IIObjectSet<TEntity>.
        //
        // Returns:
        //     The new System.Data.Objects.IIObjectSet<TEntity> instance.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Data.Objects.ObjectContext.DefaultContainerName property is not
        //     set on the System.Data.Objects.ObjectContext.-or-The specified type belongs
        //     to more than one entity set.
        ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class;
        //                                        
        // Summary:
        //     Creates a new System.Data.Objects.IIObjectSet<TEntity> instance that is used
        //     to query, add, modify, and delete objects of the specified type and with
        //     the specified entity set name.
        //
        // Parameters:
        //   entitySetName:
        //     Name of the entity set for the returned System.Data.Objects.IIObjectSet<TEntity>.
        //     The string must be qualified by the default container name if the System.Data.Objects.ObjectContext.DefaultContainerName
        //     property is not set on the System.Data.Objects.ObjectContext.
        //
        // Type parameters:
        //   TEntity:
        //     Entity type of the requested System.Data.Objects.IIObjectSet<TEntity>.
        //
        // Returns:
        //     The new System.Data.Objects.IIObjectSet<TEntity> instance.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Data.Metadata.Edm.EntitySet from entitySetName does not match
        //     the System.Data.Metadata.Edm.EntitySet of the object’s System.Data.EntityKey.-or-The
        //     System.Data.Objects.ObjectContext.DefaultContainerName property is not set
        //     on the System.Data.Objects.ObjectContext and the name is not qualified as
        //     part of the entitySetName parameter.-or-The specified type belongs to more
        //     than one entity set.
        ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class;
        //
        // Summary:
        //     Generates an equivalent type that can be used with the Entity Framework for
        //     each type in the supplied enumeration.
        //
        // Parameters:
        //   types:
        //     An enumeration of System.Type objects that represent custom data classes
        //     that map to the conceptual model.
        void CreateProxyTypes(IEnumerable<Type> types);
        //
        // Summary:
        //     Creates an System.Data.Objects.ObjectQuery<T> in the current object context
        //     by using the specified query string.
        //
        // Parameters:
        //   queryString:
        //     The query string to be executed.
        //
        //   parameters:
        //     Parameters to pass to the query.
        //
        // Type parameters:
        //   T:
        //     The entity type of the returned System.Data.Objects.ObjectQuery<T>.
        //
        // Returns:
        //     An System.Data.Objects.ObjectQuery<T> of the specified type.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The queryString or parameters parameter is null.
        ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters);
        //
        // Summary:
        //     Checks if the database that is specified as the database in the current data
        //     source connection exists on the data source.
        //
        // Returns:
        //     true if the database exists.
        bool DatabaseExists();
        //
        // Summary:
        //     Deletes the database that is specified as the database in the current data
        //     source connection.
        void DeleteDatabase();
        //
        // Summary:
        //     Marks an object for deletion.
        //
        // Parameters:
        //   entity:
        //     An object that specifies the entity to delete. The object can be in any state
        //     except System.Data.EntityState.Detached.
        void DeleteObject(object entity);
        //
        // Summary:
        //     Removes the object from the object context.
        //
        // Parameters:
        //   entity:
        //     Object to be detached. Only the entity is removed; if there are any related
        //     objects that are being tracked by the same System.Data.Objects.ObjectStateManager,
        //     those will not be detached automatically.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The entity is null.
        //
        //   System.InvalidOperationException:
        //     The entity is not associated with this System.Data.Objects.ObjectContext
        //     (for example, was newly created and not associated with any context yet,
        //     or was obtained through some other context, or was already detached).
        void Detach(object entity);
        //
        // Summary:
        //     Ensures that System.Data.Objects.ObjectStateEntry changes are synchronized
        //     with changes in all objects that are tracked by the System.Data.Objects.ObjectStateManager.
        void DetectChanges();
        //
        // Summary:
        //     Releases the resources used by the object context.
        void Dispose();

        //
        // Summary:
        //     Executes a stored procedure or function that is defined in the data source
        //     and expressed in the conceptual model; discards any results returned from
        //     the function; and returns the number of rows affected by the execution.
        //
        // Parameters:
        //   functionName:
        //     The name of the stored procedure or function. The name can include the container
        //     name, such as <Container Name>.<Function Name>. When the default container
        //     name is known, only the function name is required.
        //
        //   parameters:
        //     An array of System.Data.Objects.ObjectParameter objects.
        //
        // Returns:
        //     The number of rows affected.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     function is null or empty -or-function is not found.
        //
        //   System.InvalidOperationException:
        //     The entity reader does not support this function.-or-There is a type mismatch
        //     on the reader and the function.
        int ExecuteFunction(string functionName, params ObjectParameter[] parameters);
        //
        // Summary:
        //     Executes a stored procedure or function that is defined in the data source
        //     and mapped in the conceptual model, with the specified parameters. Returns
        //     a typed System.Data.Objects.ObjectResult<T>.
        //
        // Parameters:
        //   functionName:
        //     The name of the stored procedure or function. The name can include the container
        //     name, such as <Container Name>.<Function Name>. When the default container
        //     name is known, only the function name is required.
        //
        //   parameters:
        //     An array of System.Data.Objects.ObjectParameter objects.
        //
        // Type parameters:
        //   TElement:
        //     The entity type of the System.Data.Objects.ObjectResult<T> returned when
        //     the function is executed against the data source. This type must implement
        //     System.Data.Objects.DataClasses.IEntityWithChangeTracker.
        //
        // Returns:
        //     An System.Data.Objects.ObjectResult<T> for the data that is returned by the
        //     stored procedure.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     function is null or empty -or-function is not found.
        //
        //   System.InvalidOperationException:
        //     The entity reader does not support this function.-or-There is a type mismatch
        //     on the reader and the function.
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters);
        //
        // Summary:
        //     Executes the given stored procedure or function that is defined in the data
        //     source and expressed in the conceptual model, with the specified parameters,
        //     and merge option. Returns a typed System.Data.Objects.ObjectResult<T>.
        //
        // Parameters:
        //   functionName:
        //     The name of the stored procedure or function. The name can include the container
        //     name, such as <Container Name>.<Function Name>. When the default container
        //     name is known, only the function name is required.
        //
        //   mergeOption:
        //     The System.Data.Objects.MergeOption to use when executing the query.
        //
        //   parameters:
        //     An array of System.Data.Objects.ObjectParameter objects.
        //
        // Type parameters:
        //   TElement:
        //     The entity type of the System.Data.Objects.ObjectResult<T> returned when
        //     the function is executed against the data source. This type must implement
        //     System.Data.Objects.DataClasses.IEntityWithChangeTracker.
        //
        // Returns:
        //     An System.Data.Objects.ObjectResult<T> for the data that is returned by the
        //     stored procedure.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     function is null or empty -or-function is not found.
        //
        //   System.InvalidOperationException:
        //     The entity reader does not support this function.-or-There is a type mismatch
        //     on the reader and the function.
        ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters);
        //
        // Summary:
        //     Executes an arbitrary command directly against the data source using the
        //     existing connection.
        //
        // Parameters:
        //   commandText:
        //     The command to execute, in the native language of the data source.
        //
        //   parameters:
        //     An array of parameters to pass to the command.
        //
        // Returns:
        //     The number of rows affected.
        int ExecuteStoreCommand(string commandText, params object[] parameters);
        //
        // Summary:
        //     Executes a query directly against the data source that returns a sequence
        //     of typed results.
        //
        // Parameters:
        //   commandText:
        //     The command to execute, in the native language of the data source.
        //
        //   parameters:
        //     An array of parameters to pass to the command.
        //
        // Type parameters:
        //   TElement:
        //
        // Returns:
        //     An enumeration of objects of type TResult.
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters);
        //
        // Summary:
        //     Executes a query directly against the data source and returns a sequence
        //     of typed results. Specify the entity set and the merge option so that query
        //     results can be tracked as entities.
        //
        // Parameters:
        //   commandText:
        //     The command to execute, in the native language of the data source.
        //
        //   entitySetName:
        //     The entity set of the TResult type. If an entity set name is not provided,
        //     the results are not going to be tracked.
        //
        //   mergeOption:
        //     The System.Data.Objects.MergeOption to use when executing the query. The
        //     default is System.Data.Objects.MergeOption.AppendOnly.
        //
        //   parameters:
        //     An array of parameters to pass to the command.
        //
        // Type parameters:
        //   TEntity:
        //
        // Returns:
        //     An enumeration of objects of type TResult.
        ObjectResult<TEntity> ExecuteStoreQuery<TEntity>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters);
        //
        // Summary:
        //     Returns all the existing proxy types.
        //
        // Returns:
        //     An System.Collections.Generic.IEnumerable<T> of all the existing proxy types.
        //IEnumerable<Type> GetKnownProxyTypes();
        //
        // Summary:
        //     Returns an object that has the specified entity key.
        //
        // Parameters:
        //   key:
        //     The key of the object to be found.
        //
        // Returns:
        //     An System.Object that is an instance of an entity type.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The key parameter is null.
        //
        //   System.Data.ObjectNotFoundException:
        //     The object is not found in either the System.Data.Objects.ObjectStateManager
        //     or the data source.
        object GetObjectByKey(EntityKey key);
        //
        // Summary:
        //     Returns the entity type of the POCO entity associated with a proxy object
        //     of a specified type.
        //
        // Parameters:
        //   type:
        //     The System.Type of the proxy object.
        //
        // Returns:
        //     The System.Type of the associated POCO entity.
        //Type GetObjectType(Type type);
        //
        // Summary:
        //     Explicitly loads an object related to the supplied object by the specified
        //     navigation property and using the default merge option.
        //
        // Parameters:
        //   entity:
        //     The entity for which related objects are to be loaded.
        //
        //   navigationProperty:
        //     The name of the navigation property that returns the related objects to be
        //     loaded.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The entity is in a System.Data.EntityState.Detached, System.Data.EntityState.Added,
        //     or System.Data.EntityState.Deleted state,-or-The entity is attached to another
        //     instance of System.Data.Objects.ObjectContext.
        void LoadProperty(object entity, string navigationProperty);
        //
        // Summary:
        //     Explicitly loads an object that is related to the supplied object by the
        //     specified LINQ query and by using the default merge option.
        //
        // Parameters:
        //   entity:
        //     The source object for which related objects are to be loaded.
        //
        //   selector:
        //     A LINQ expression that defines the related objects to be loaded.
        //
        // Type parameters:
        //   TEntity:
        //
        // Exceptions:
        //   System.ArgumentException:
        //     selector does not supply a valid input parameter.
        //
        //   System.ArgumentNullException:
        //     selector is null.
        //
        //   System.InvalidOperationException:
        //     The entity is in a System.Data.EntityState.Detached, System.Data.EntityState.Added,
        //     or System.Data.EntityState.Deleted state,-or-The entity is attached to another
        //     instance of System.Data.Objects.ObjectContext.
        void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector);
        //
        // Summary:
        //     Explicitly loads an object that is related to the supplied object by the
        //     specified navigation property and using the specified merge option.
        //
        // Parameters:
        //   entity:
        //     The entity for which related objects are to be loaded.
        //
        //   navigationProperty:
        //     The name of the navigation property that returns the related objects to be
        //     loaded.
        //
        //   mergeOption:
        //     The System.Data.Objects.MergeOption value to use when you load the related
        //     objects.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The entity is in a System.Data.EntityState.Detached, System.Data.EntityState.Added,
        //     or System.Data.EntityState.Deleted state,-or-The entity is attached to another
        //     instance of System.Data.Objects.ObjectContext.
        void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption);
        //
        // Summary:
        //     Explicitly loads an object that is related to the supplied object by the
        //     specified LINQ query and by using the specified merge option.
        //
        // Parameters:
        //   entity:
        //     The source object for which related objects are to be loaded.
        //
        //   selector:
        //     A LINQ expression that defines the related objects to be loaded.
        //
        //   mergeOption:
        //     The System.Data.Objects.MergeOption value to use when you load the related
        //     objects.
        //
        // Type parameters:
        //   TEntity:
        //
        // Exceptions:
        //   System.ArgumentException:
        //     selector does not supply a valid input parameter.
        //
        //   System.ArgumentNullException:
        //     selector is null.
        //
        //   System.InvalidOperationException:
        //     The entity is in a System.Data.EntityState.Detached, System.Data.EntityState.Added,
        //     or System.Data.EntityState.Deleted state,-or-The entity is attached to another
        //     instance of System.Data.Objects.ObjectContext.
        void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption);
        //
        // Summary:
        //     Updates a collection of objects in the object context with data from the
        //     data source.
        //
        // Parameters:
        //   refreshMode:
        //     A System.Data.Objects.RefreshMode value that indicates whether property changes
        //     in the object context are overwritten with property values from the data
        //     source.
        //
        //   collection:
        //     An System.Collections.IEnumerable collection of objects to refresh.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     collection is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     refreshMode is not valid.
        //
        //   System.ArgumentException:
        //     collection is empty. -or- An object is not attached to the context.
        void Refresh(RefreshMode refreshMode, IEnumerable collection);
        //
        // Summary:
        //     Updates an object in the object context with data from the data source.
        //
        // Parameters:
        //   refreshMode:
        //     One of the System.Data.Objects.RefreshMode values that specifies which mode
        //     to use for refreshing the System.Data.Objects.ObjectStateManager.
        //
        //   entity:
        //     The object to be refreshed.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     collection is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     refreshMode is not valid.
        //
        //   System.ArgumentException:
        //     collection is empty. -or- An object is not attached to the context.
        void Refresh(RefreshMode refreshMode, object entity);
        //
        // Summary:
        //     Persists all updates to the data source and resets change tracking in the
        //     object context.
        //
        // Returns:
        //     The number of objects in an System.Data.EntityState.Added, System.Data.EntityState.Modified,
        //     or System.Data.EntityState.Deleted state when System.Data.Objects.ObjectContext.SaveChanges()
        //     was called.
        //
        // Exceptions:
        //   System.Data.OptimisticConcurrencyException:
        //     An optimistic concurrency violation has occurred in the data source.
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        int SaveChanges();
        //
        // Summary:
        //     Persists all updates to the data source and optionally resets change tracking
        //     in the object context.
        //
        // Parameters:
        //   acceptChangesDuringSave:
        //     This parameter is needed for client-side transaction support. If true, the
        //     change tracking on all objects is reset after System.Data.Objects.ObjectContext.SaveChanges(System.Boolean)
        //     finishes. If false, you must call the System.Data.Objects.ObjectContext.AcceptAllChanges()
        //     method after System.Data.Objects.ObjectContext.SaveChanges(System.Boolean).
        //
        // Returns:
        //     The number of objects in an System.Data.EntityState.Added, System.Data.EntityState.Modified,
        //     or System.Data.EntityState.Deleted state when System.Data.Objects.ObjectContext.SaveChanges()
        //     was called.
        //
        // Exceptions:
        //   System.Data.OptimisticConcurrencyException:
        //     An optimistic concurrency violation has occurred.
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use SaveChanges(SaveOptions options) instead.")]
        int SaveChanges(bool acceptChangesDuringSave);
        //
        // Summary:
        //     Persists all updates to the data source with the specified System.Data.Objects.SaveOptions.
        //
        // Parameters:
        //   options:
        //     A System.Data.Objects.SaveOptions value that determines the behavior of the
        //     operation.
        //
        // Returns:
        //     The number of objects in an System.Data.EntityState.Added, System.Data.EntityState.Modified,
        //     or System.Data.EntityState.Deleted state when System.Data.Objects.ObjectContext.SaveChanges()
        //     was called.
        //
        // Exceptions:
        //   System.Data.OptimisticConcurrencyException:
        //     An optimistic concurrency violation has occurred.
        int SaveChanges(SaveOptions options);
        //
        // Summary:
        //     Translates a System.Data.Common.DbDataReader that contains rows of entity
        //     data to objects of the requested entity type.
        //
        // Parameters:
        //   reader:
        //     The System.Data.Common.DbDataReader that contains entity data to translate
        //     into entity objects.
        //
        // Type parameters:
        //   TElement:
        //
        // Returns:
        //     An enumeration of objects of type TResult.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     When reader is null.
        ObjectResult<TElement> Translate<TElement>(DbDataReader reader);
        //
        // Summary:
        //     Translates a System.Data.Common.DbDataReader that contains rows of entity
        //     data to objects of the requested entity type, in a specific entity set, and
        //     with the specified merge option.
        //
        // Parameters:
        //   reader:
        //     The System.Data.Common.DbDataReader that contains entity data to translate
        //     into entity objects.
        //
        //   entitySetName:
        //     The entity set of the TResult type.
        //
        //   mergeOption:
        //     The System.Data.Objects.MergeOption to use when translated objects are added
        //     to the object context. The default is System.Data.Objects.MergeOption.AppendOnly.
        //
        // Type parameters:
        //   TEntity:
        //
        // Returns:
        //     An enumeration of objects of type TResult.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     When reader is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     When the supplied mergeOption is not a valid System.Data.Objects.MergeOption
        //     value.
        //
        //   System.InvalidOperationException:
        //     When the supplied entitySetName is not a valid entity set for the TResult
        //     type.
        ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption);
        //
        // Summary:
        //     Returns an object that has the specified entity key.
        //
        // Parameters:
        //   key:
        //     The key of the object to be found.
        //
        //   value:
        //     When this method returns, contains the object.
        //
        // Returns:
        //     true if the object was retrieved successfully. false if the key is temporary,
        //     the connection is null, or the value is null.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     Incompatible metadata for key.
        //
        //   System.ArgumentNullException:
        //     key is null.
        bool TryGetObjectByKey(EntityKey key, out object value);
        #endregion
    }
}
