using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;
public static class ProjectTypes
{
    public const string Fiction = "Fiction";
    public const string Nonfiction = "Nonfiction";

    public static readonly string[] All = new string[] {Fiction, Nonfiction};
}
