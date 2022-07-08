// -----------------------------------------------------------------------
// <copyright file="UpperpaneSearchKeyDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class UpperpaneSearchKeyDTO
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public string searchKey { get; set; }
    }
}
