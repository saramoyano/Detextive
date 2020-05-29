using System;

namespace Gma.CodeCloud.Controls.TextAnalyses.Extractors
{
    public interface IProgressIndicator
    {
        Double Maximum { get; set; }
        void Increment(int value);
    }
}
