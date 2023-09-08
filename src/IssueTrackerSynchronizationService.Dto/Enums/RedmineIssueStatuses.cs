namespace IssueTrackerSynchronizationService.Dto.Enums
{
    /// <summary>
    /// Статусы задач в Redmine
    /// </summary>
    public enum RedmineIssueStatuses
    {
        /// <summary>
        /// Закрыт
        /// </summary>
        Closed = 5,

        /// <summary>
        /// В работе
        /// </summary>
        InWork = 10,

        /// <summary>
        /// Тестирование
        /// </summary>
        Testing = 11,

        /// <summary>
        /// Отладка
        /// </summary>
        Debugging = 12,

        /// <summary>
        /// Code Review
        /// </summary>
        CodeReview = 15
    }
}
