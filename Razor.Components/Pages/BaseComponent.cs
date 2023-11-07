using DataModel.Utility;
using Microsoft.AspNetCore.Components;

namespace Razor.Components.Pages
{
    public class BaseComponent : ComponentBase
    {
        protected string AddAnimation = AnimationName.FadeDown;
        protected string Duration = "4000";
        protected string Delay = "50";
   }
}