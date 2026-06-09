/// <summary>
/// Entry point for the Content Builder project, 
/// which when executed will build content according to the "Content Collection Strategy" defined in the MyContentCollector class.
/// </summary>
/// <remarks>
/// Make sure to validate the directory paths in the "ContentBuilderParams" for your specific project.
/// For more details regarding the Content Builder, see the MonoGame documentation: <tbc.>
/// </remarks>

using MonoGame.Framework.Content.Pipeline.Builder;

namespace SummerGame.Content;

public class MyContentCollector : ContentBuilder
{
    public override IContentCollection GetContentCollection()
    {
        var contentCollection = new ContentCollection();

        // override .txt files to be copied
        contentCollection.IncludeCopy<RegexRule>(".txt");
        contentCollection.Include<RegexRule>(".ttf");
        contentCollection.Include<RegexRule>(".png");

        // exclude bin / obj paths
        contentCollection.Exclude<RegexRule>("bin/");
        contentCollection.Exclude<RegexRule>("obj/");
        contentCollection.Exclude<WildcardRule>("*.mgcb");
        contentCollection.Exclude<WildcardRule>("*.contentproj");
        contentCollection.Exclude<WildcardRule>("*.xnb");
        contentCollection.Exclude<WildcardRule>("*.mgcontent");

        return contentCollection;
    }
}


