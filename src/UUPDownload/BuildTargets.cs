﻿// Copyright (c) Gustave Monce and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using CompDB;
using MediaCreationLib.Planning.NET;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsUpdateLib;

namespace UUPDownload
{
    public static class BuildTargets
    {
        public class EditionPlanningWithLanguage
        {
            public List<EditionTarget> EditionTargets;
            public string LanguageCode;
        }

        public static async Task<EditionPlanningWithLanguage> GetTargetedPlanAsync(this UpdateData update, string LanguageCode)
        {
            var compDBs = await update.GetCompDBsAsync();
            CompDBXmlClass.Package editionPackPkg = compDBs.GetEditionPackFromCompDBs();

            string editionPkg = await update.DownloadFileFromDigestAsync(editionPackPkg.Payload.PayloadItem.PayloadHash);
            return await update.GetTargetedPlanAsync(LanguageCode, editionPkg);
        }

        public static async Task<EditionPlanningWithLanguage> GetTargetedPlanAsync(this UpdateData update, string LanguageCode, string editionPkg)
        {
            var compDBs = await update.GetCompDBsAsync();
            if (string.IsNullOrEmpty(editionPkg))
            {
                return null;
            }

            List<EditionTarget> targets;
            _ = ConversionPlanBuilder.GetTargetedPlan(compDBs, editionPkg, LanguageCode, out targets, null);
            return new EditionPlanningWithLanguage() { EditionTargets = targets, LanguageCode = LanguageCode };
        }

        public static void PrintAvailablePlan(this List<EditionTarget> targets)
        {
            foreach (var target in targets)
            {
                foreach (var str in ConversionPlanBuilder.PrintEditionTarget(target))
                    Logging.Log(str);
            }
        }
    }
}
