﻿namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Collects all possible @-rules for easy access.
    /// </summary>
    [DebuggerStepThrough]
    static class CssAtRule
    {
        delegate CssRule Creator(CssParser parser);

        static readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>();

        static CssAtRule()
        {
            creators.Add(RuleNames.Charset, CssParser.CreateCharsetRule);
            creators.Add(RuleNames.Page, CssParser.CreatePageRule);
            creators.Add(RuleNames.Import, CssParser.CreateImportRule);
            creators.Add(RuleNames.FontFace, CssParser.CreateFontFaceRule);
            creators.Add(RuleNames.Media, CssParser.CreateMediaRule);
            creators.Add(RuleNames.Namespace, CssParser.CreateNamespaceRule);
            creators.Add(RuleNames.Supports, CssParser.CreateSupportsRule);
            creators.Add(RuleNames.Keyframes, CssParser.CreateKeyframesRule);
            creators.Add(RuleNames.Document, CssParser.CreateDocumentRule);
        }

        /// <summary>
        /// Parses an @-rule with the given name, if there is any.
        /// </summary>
        /// <param name="parser">The currently active parser.</param>
        /// <param name="name">The name of the @-rule.</param>
        /// <returns>The created rule or null, if no rule could be created.</returns>
        public static CssRule CreateAtRule(this CssParser parser, String name)
        {
            Creator creator;

            if (creators.TryGetValue(name, out creator))
                return creator(parser);

            return null;
        }
    }
}
