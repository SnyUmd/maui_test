namespace Hct101
{
    public class ToolCtrl
    {
        public void scroll_ScrollView(ScrollView sv, double x, double y, bool bl_animation)
        {
            sv.ScrollToAsync(x, y, bl_animation);
        }

        public void scroll_ScrollView(ScrollView sv, Element element_, ScrollToPosition position_, bool bl_animation)
        {
            sv.ScrollToAsync(element_, position_, bl_animation);
        }
    }
}
