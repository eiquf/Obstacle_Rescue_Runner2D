public class SocialMediaActions
{
    public IButtonAction GetSocialMediaAction(int index)
    {
        return index switch
        {
            UIButtonsCount.Inst => new OpenLinkAction("https://www.instagram.com/eiquif/"),
            UIButtonsCount.Twitter => new OpenLinkAction("https://x.com/eiquif"),
            UIButtonsCount.Telegram => new OpenLinkAction("https://t.me/eiquf"),
            _ => null
        };
    }
}