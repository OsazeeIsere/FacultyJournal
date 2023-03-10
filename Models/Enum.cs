﻿namespace FacultyJournal.Models
{
    public class Enum
    {
        public enum ArticleStatus
        {
            Pending,
            Accepted_For_Review,
            Rejected_Before_Review,
            Review_In_Progress,
            Review_Completed,
            Rejected_After_Review,
            Accepted_For_Publication,
            Published

        }
        public enum Title
        {
            Mr,
            Mrs,
            Ms,
            Dr,
            Prof
        }
        //Post Graduate Progress Report Ranking
        public enum Ranking
        {
            Pending,
            Satisfied,
            Partially_Satisfied,
            Not_Satisfied
        }
        public enum PgProgram
        {
            PGD,
            MSc,
            MPhil,
            MPhil_PhD,
            PhD
        }
        public enum WorksStatus
        {
            Pending,
            In_Progress,
            Completed
        }
        public enum EditorRole
        {
            ArticleReviewerRole,
            ArticleSuperAdminRole
        }
    }
}
