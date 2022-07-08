// -----------------------------------------------------------------------
// <copyright file="StoryMapResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class StoryMapResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<StoryMapDTO> storyMapList { get; set; }

        public string Narration { get; set; }
    }

    public class SuperStoryResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public List<List<SuperStoryMapDTO>> UnderlyingItemGroups { get; set; }
        public List<List<SuperStoryMapDTO>> NeedForFocusItemGroups { get; set; }
        public List<List<SuperStoryMapDTO>> NeedInBackgroundItemGroups { get; set; }
        public List<List<SuperStoryMapDTO>> StrengthItemGroups { get; set; }
        public List<List<SuperStoryMapDTO>> CircumstancesItemGroups { get; set; }
        public List<List<SuperStoryMapDTO>> GoalsItemGroups { get; set; }
        public List<List<SuperStoryMapDTO>> OpinionItemGroups { get; set; }

    }
    public class SuperStoryGroup
    {
        public string InstrumentAbbrev { get; set; }
        public string VoiceTypeInDetail { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public List<SuperStoryMapDTO> superStoryMapDTO { get; set; }
    }
}
