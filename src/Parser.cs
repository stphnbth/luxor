using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

using Extensions;
using static Reference.DataTables;

namespace Luxor
{
    public class Parser
    {
        private Mode _mode;
        
        private int _scriptNestingLevel;
        private bool _pause;

        public void run()
        {
            while (true)
            {
                switch (_mode)
                {
                    case Mode.Initial:

                        break;
                    case Mode.BeforeHTML:

                        break;
                    case Mode.BeforeHead:

                        break;
                    case Mode.Head:

                        break;
                    case Mode.HeadNoScript:

                        break;
                    case Mode.AfterHead:

                        break;
                    case Mode.Body:

                        break;
                    case Mode.Text:

                        break;
                    case Mode.Table:

                        break;
                    case Mode.TableText:

                        break;
                    case Mode.Caption:

                        break;
                    case Mode.ColumnGroup:

                        break;
                    case Mode.TableBody:

                        break;
                    case Mode.Row:

                        break;
                    case Mode.Cell:

                        break;
                    case Mode.Select:

                        break;
                    case Mode.SelectInTable:

                        break;
                    case Mode.Template:

                        break;
                    case Mode.AfterBody:

                        break;
                    case Mode.Frameset:

                        break;
                    case Mode.AfterFrameset:

                        break;
                    case Mode.AfterAfterBody:

                        break;
                    case Mode.AfterAfterFrameset:

                        break;
                    default:
                        break;
                }
            }
        }
    }
}