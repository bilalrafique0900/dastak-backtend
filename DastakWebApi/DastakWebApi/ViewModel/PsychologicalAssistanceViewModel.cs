using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{

    public class PsychologicalAssistanceViewModel
    {
    public string? ReferenceNo { get; set; }
        public string? NameOfResident { get; set; }
        public int? Age { get; set; }
        public string? PsychologicalAssessment { get; set; }
        public string? WhatArrangementsMadeForImmediateAssistance { get; set; }
        public string? NatureOfAssistance { get; set; }
        public DateTime? SoughtAt { get; set; }
        public DateTime? ProvidedAt { get; set; }
        public string? NameOfConsultant { get; set; }
        public string? LocationOfConsultant { get; set; }
        public string? Contact { get; set; }
        public string? Notes { get; set; }
        public DateTime? ConductedAt { get; set; }
        public TimeSpan? StartedAt { get; set; }
        public TimeSpan? EndedAt { get; set; }
        // Properties related to childs
        public string? Name { get; set; }
        public string? ChildAge { get; set; }
        // Properties related to childs
        public string? PsychologicalAssistanceProvidedTo { get; set; }
        public List<ChildpsychologicalAssistance> ChildPsychologicalAssistances { get; set; }
    }




}
