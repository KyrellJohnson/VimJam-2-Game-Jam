namespace Edgar.Unity
{
    /// <summary>
    /// Interface for custom room template outline handlers.
    /// </summary>
    public interface IRoomTemplateOutlineHandler
    {
        /// <summary>
        /// Gets the outline of the room template.
        /// </summary>
        /// <returns></returns>
        Polygon2D GetRoomTemplateOutline();
    }
}