﻿using System.Windows;
using System.Collections.Generic;
using DevExpress.Data;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid;
// ...

namespace CustomSummary_EmptyCells {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        List<TestData> list;
        Dictionary<int, bool> selectedValues = new Dictionary<int, bool>();

        public Window1() {
            InitializeComponent();
            list = new List<TestData>();
            for (int i = 0; i < 100; i++) {
                list.Add(new TestData() { Text = "group " + i % 10, Number = i });
                if (i % 3 == 0)
                    list[i].Number = null;
            }
            grid.DataSource = list;
        }

        int emptyCellsTotalCount = 0;
        int emptyCellsGroupCount = 0;

        void grid_CustomSummary(object sender, CustomSummaryEventArgs e) {
            if (((GridSummaryItem)e.Item).FieldName != "Number")
                return;
            if (e.IsTotalSummary) {
                if (e.SummaryProcess == CustomSummaryProcess.Start) {
                    emptyCellsTotalCount = 0;
                }
                if (e.SummaryProcess == CustomSummaryProcess.Calculate) {
                    int? val = (int?)e.FieldValue;
                    if (!val.HasValue)
                        emptyCellsTotalCount++;
                    e.TotalValue = emptyCellsTotalCount;
                }
            }
            if (e.IsGroupSummary) {
                if (e.SummaryProcess == CustomSummaryProcess.Start) {
                    emptyCellsGroupCount = 0;
                }
                if (e.SummaryProcess == CustomSummaryProcess.Calculate) {
                    int? val = (int?)e.FieldValue;
                    if (!val.HasValue)
                        emptyCellsGroupCount++;
                    e.TotalValue = emptyCellsGroupCount;
                }
            }
        }

        public class TestData {
            public string Text { get; set; }
            public int? Number { get; set; }
        }
    }
}
