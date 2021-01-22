using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazoring.PWA.Client.Components
{
    public class BlzGender : ComponentBase
    {
        [Parameter]
        public PWA.Shared.Models.Gender? Gender { get; set; }
        /// <summary>
        /// Font Size, 30 is default
        /// </summary>
        [Parameter]
        public int FontSize { get; set; } = 30;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int i = 0;
            builder.OpenElement(i, "i");
            string classAttributes = "fas ";

            if (!Gender.HasValue)
            {
                classAttributes += "fa-user-slash";
            }
            else
            {
                if (Gender == PWA.Shared.Models.Gender.Male)
                    classAttributes += "fa-male";
                else
                    classAttributes += "fa-female";
            }
            builder.AddAttribute(++i, "class", classAttributes);
            builder.AddAttribute(++i, "style", $"font-size: {FontSize}px;");
            builder.CloseElement();
        }
    }
}