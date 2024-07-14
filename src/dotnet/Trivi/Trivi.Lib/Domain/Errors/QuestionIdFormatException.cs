using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivi.Lib.Domain.Errors;

public class QuestionIdFormatException() : FormatException("Unrecognized QuestionId format")
{
    
}
