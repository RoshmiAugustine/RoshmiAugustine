// -----------------------------------------------------------------------
// <copyright file="Item.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.Entities
{
    public class Item : BaseEntity
    {
        public int ItemID { get; set; }
        public Guid ItemIndex { get; set; }
        public string Abbreviation { get; set; }
        public int ItemResponseTypeID { get; set; }
        public string? Label { get; set; }
        public string Name { get; set; }
        public string? Considerations { get; set; }
        public string Description { get; set; }
        public string SupplementalDescription { get; set; }
        public int ResponseValueTypeID { get; set; }
        public int ListOrder { get; set; }
        public bool UseRequiredConfidentiality { get; set; }
        public bool UsePersonRequestedConfidentiality { get; set; }
        public bool UseOtherConfidentiality { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public bool? DefaultRequiredConfidentiality { get; set; }
        public bool? DefaultPersonRequestedConfidentiality { get; set; }
        public bool? DefaultOtherConfidentiality { get; set; }
        public int? DefaultResponseID { get; set; }
        public int? ParentItemID { get; set; }        
        public bool AllowMultipleGroups { get; set; }
        public bool AutoExpand { get; set; }
        public bool ResponseRequired { get; set; }
        public bool ShowNotes { get; set; }
        public bool NotesRequired { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsExpandable { get; set; } = true;
        public bool GridLayoutInFormView { get; set; }
        public int? ChildItemGroupNumber { get; set; }

        public ItemResponseType ItemResponseType { get; set; }
        public ResponseValueType ResponseValueType { get; set; }
        public User UpdateUser { get; set; }
        public Response DefaultResponse { get; set; }
        public Item ParentItem { get; set; }
    }
}
