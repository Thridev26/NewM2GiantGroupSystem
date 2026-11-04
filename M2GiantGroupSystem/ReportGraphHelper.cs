
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace M2GiantGroupSystem
{
    public static class ReportGraphHelper
    {
        private static readonly Color DarkGreen =
            Color.FromArgb(27, 67, 50);

        private static readonly Color MediumGreen =
            Color.FromArgb(45, 106, 79);

        private static readonly Color LightGreen =
            Color.FromArgb(82, 183, 136);

        private static readonly Color PaleGreen =
            Color.FromArgb(216, 243, 220);

        public static void Attach(
            TabControl tabs,
            string reportType,
            Func<DataSet> getData,
            Func<bool> validateDates)
        {
            TabPage graphPage = new TabPage("Visual Analytics");
            graphPage.BackColor = Color.FromArgb(242, 247, 244);
            graphPage.Padding = new Padding(15);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = graphPage.BackColor
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            FlowLayoutPanel toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = graphPage.BackColor,
                Padding = new Padding(8)
            };

            Label heading = new Label
            {
                Text = reportType + " Analytics",
                AutoSize = true,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                ForeColor = DarkGreen,
                Margin = new Padding(5, 8, 25, 0)
            };

            Button refresh = new Button
            {
                Text = "Refresh Graph",
                Width = 170,
                Height = 40,
                BackColor = MediumGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            refresh.FlatAppearance.BorderSize = 0;

            toolbar.Controls.Add(heading);
            toolbar.Controls.Add(refresh);

            Chart chart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Palette = ChartColorPalette.EarthTones,
                BorderlineColor = PaleGreen,
                BorderlineWidth = 1,
                BorderlineDashStyle = ChartDashStyle.Solid
            };

            chart.ChartAreas.Add(new ChartArea("MainArea"));

            chart.Legends.Add(new Legend("MainLegend")
            {
                Docking = Docking.Bottom,
                Font = new Font("Segoe UI", 10)
            });

            layout.Controls.Add(toolbar, 0, 0);
            layout.Controls.Add(chart, 0, 1);

            graphPage.Controls.Add(layout);
            tabs.TabPages.Add(graphPage);

            Action refreshGraph = () =>
            {
                try
                {
                    if (!validateDates())
                    {
                        MessageBox.Show(
                            "The start date must not be after the end date.",
                            "Invalid Date Range",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    DataSet ds = getData();

                    if (ds == null ||
                        ds.Tables.Count == 0 ||
                        ds.Tables[0].Rows.Count == 0)
                    {
                        chart.Series.Clear();
                        chart.Titles.Clear();
                        chart.Titles.Add(
                            "No data found for the selected filters.");
                        return;
                    }

                    BuildChart(chart, reportType, ds.Tables[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Unable to load the graph:\n" + ex.Message,
                        "Graph Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };

            refresh.Click += (s, e) => refreshGraph();
            graphPage.Enter += (s, e) => refreshGraph();
        }

        private static double Number(DataRow row, string field)
        {
            if (!row.Table.Columns.Contains(field) ||
                row[field] == DBNull.Value)
                return 0;

            return Convert.ToDouble(row[field]);
        }

        private static string StringValue(
            DataRow row, string field)
        {
            if (!row.Table.Columns.Contains(field) ||
                row[field] == DBNull.Value)
                return "Unknown";

            return Convert.ToString(row[field]);
        }

        private static void AddSeries(
            Chart chart,
            string name,
            SeriesChartType type,
            Color color)
        {
            Series series = new Series(name)
            {
                ChartType = type,
                ChartArea = "MainArea",
                Legend = "MainLegend",
                Color = color,
                BorderWidth = 3,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9)
            };

            chart.Series.Add(series);
        }

        private static void BuildChart(
            Chart chart,
            string reportType,
            DataTable data)
        {
            chart.Series.Clear();
            chart.Titles.Clear();

            chart.Titles.Add(reportType + " Overview");
            chart.Titles[0].Font =
                new Font("Segoe UI", 16, FontStyle.Bold);
            chart.Titles[0].ForeColor = DarkGreen;

            ChartArea area = chart.ChartAreas["MainArea"];
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(225, 232, 228);
            area.AxisX.LabelStyle.Font =
                new Font("Segoe UI", 9);
            area.AxisY.LabelStyle.Font =
                new Font("Segoe UI", 9);
            area.AxisX.LabelStyle.Angle = -35;

            switch (reportType)
            {
                case "Income":
                    BuildIncome(chart, data);
                    break;

                case "Expenses":
                    BuildExpenses(chart, data);
                    break;

                case "Profits":
                    BuildProfits(chart, data);
                    break;

                case "Service Profitability":
                    BuildServiceProfitability(chart, data);
                    break;

                case "Quote Conversion":
                    BuildQuoteConversion(chart, data);
                    break;

                case "Client Value":
                    BuildClientValue(chart, data);
                    break;
            }

            chart.Invalidate();
        }

        private static void BuildIncome(
            Chart chart, DataTable data)
        {
            AddSeries(
                chart, "Income",
                SeriesChartType.Column, MediumGreen);

            SortedDictionary<DateTime, double> totals =
                new SortedDictionary<DateTime, double>();

            foreach (DataRow row in data.Rows)
            {
                DateTime date =
                    Convert.ToDateTime(row["paymentDate"]).Date;

                double amount = Number(row, "amountPaid");

                if (!totals.ContainsKey(date))
                    totals[date] = 0;

                totals[date] += amount;
            }

            foreach (var item in totals)
            {
                chart.Series["Income"].Points.AddXY(
                    item.Key.ToString("dd MMM yyyy"),
                    item.Value);
            }

            chart.ChartAreas[0].AxisY.Title = "Income (R)";
        }

        private static void BuildExpenses(
            Chart chart, DataTable data)
        {
            AddSeries(
                chart, "Expenses",
                SeriesChartType.Doughnut, MediumGreen);

            double fuel = 0;
            double dumping = 0;

            foreach (DataRow row in data.Rows)
            {
                fuel += Number(row, "totalFuelCost");
                dumping += Number(row, "dumpingCost");
            }

            Series series = chart.Series["Expenses"];
            series["DoughnutRadius"] = "60";

            if (fuel > 0)
            {
                int i = series.Points.AddXY("Fuel", fuel);
                series.Points[i].Color = MediumGreen;
                series.Points[i].LegendText = "Fuel";
            }

            if (dumping > 0)
            {
                int i = series.Points.AddXY("Dumping", dumping);
                series.Points[i].Color = LightGreen;
                series.Points[i].LegendText = "Dumping";
            }

            if (series.Points.Count == 0)
                chart.Titles.Add("No expenses recorded.");
        }

        private static void BuildProfits(
            Chart chart, DataTable data)
        {
            AddSeries(
                chart, "Income",
                SeriesChartType.Line, LightGreen);

            AddSeries(
                chart, "Expenses",
                SeriesChartType.Line, Color.Orange);

            AddSeries(
                chart, "Profit",
                SeriesChartType.Line, MediumGreen);

            SortedDictionary<DateTime, double[]> totals =
                new SortedDictionary<DateTime, double[]>();

            foreach (DataRow row in data.Rows)
            {
                DateTime date =
                    Convert.ToDateTime(row["JobDate"]).Date;

                if (!totals.ContainsKey(date))
                    totals[date] = new double[2];

                totals[date][0] += Number(row, "Income");

                totals[date][1] +=
                    Number(row, "FuelCost") +
                    Number(row, "DumpingCost");
            }

            foreach (var item in totals)
            {
                string date = item.Key.ToString("dd MMM yyyy");

                double income = item.Value[0];
                double expenses = item.Value[1];
                double profit = income - expenses;

                chart.Series["Income"].Points.AddXY(
                    date, income);

                chart.Series["Expenses"].Points.AddXY(
                    date, expenses);

                chart.Series["Profit"].Points.AddXY(
                    date, profit);
            }

            chart.ChartAreas[0].AxisY.Title = "Amount (R)";
        }


        private static void BuildServiceProfitability(
            Chart chart, DataTable data)
        {
            AddSeries(
                chart,
                "Profit",
                SeriesChartType.Bar,
                Color.FromArgb(45, 106, 79));

            Series series = chart.Series["Profit"];

            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N2";
            series.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            series["PointWidth"] = "0.55";

            decimal minimumProfit = 0;
            decimal maximumProfit = 0;

            foreach (DataRow row in data.Rows)
            {
                string jobType = StringValue(row, "JobType");

                decimal revenue =
                    Convert.ToDecimal(Number(row, "TotalRevenue"));

                decimal fuel =
                    Convert.ToDecimal(Number(row, "FuelCost"));

                decimal dumping =
                    Convert.ToDecimal(Number(row, "DumpingCost"));

                decimal assets =
                    Convert.ToDecimal(Number(row, "AssetCost"));

                decimal totalCost = fuel + dumping + assets;
                decimal profit = revenue - totalCost;

                int index = series.Points.AddXY(
                    jobType,
                    Convert.ToDouble(profit));

                series.Points[index].Color =
                    profit >= 0
                        ? Color.FromArgb(45, 106, 79)
                        : Color.FromArgb(200, 70, 70);

                series.Points[index].ToolTip =
                    jobType +
                    "\nRevenue: R" + revenue.ToString("N2") +
                    "\nCosts: R" + totalCost.ToString("N2") +
                    "\nProfit: R" + profit.ToString("N2");

                if (profit < minimumProfit)
                    minimumProfit = profit;

                if (profit > maximumProfit)
                    maximumProfit = profit;
            }

            ChartArea area = chart.ChartAreas["MainArea"];

            // For horizontal bar charts:
            // AxisX = service categories
            // AxisY = numerical profit values

            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = 0;

            area.AxisY.Title = "Profit (R)";
            area.AxisY.LabelStyle.Format = "N0";

            // Display zero and leave room for the bars
            double min = Convert.ToDouble(minimumProfit);
            double max = Convert.ToDouble(maximumProfit);

            area.AxisY.Minimum =
                min < 0 ? min * 1.15 : 0;

            area.AxisY.Maximum =
                max > 0 ? max * 1.15 : 1;

            area.AxisY.Crossing = 0;

            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(225, 232, 228);

            area.RecalculateAxesScale();

            // Apply explicit bounds after recalculating
            area.AxisY.Minimum =
                min < 0 ? min * 1.15 : 0;

            area.AxisY.Maximum =
                max > 0 ? max * 1.15 : 1;

            chart.Invalidate();
        }


        private static void BuildQuoteConversion(
            Chart chart, DataTable data)
        {
            AddSeries(
                chart, "Conversion Rate",
                SeriesChartType.Bar, MediumGreen);

            foreach (DataRow row in data.Rows)
            {
                double requests =
                    Number(row, "TotalRequests");

                double accepted =
                    Number(row, "QuotesAccepted");

                double rate = requests > 0
                    ? accepted / requests * 100
                    : 0;

                chart.Series["Conversion Rate"].Points.AddXY(
                    StringValue(row, "RequestSource"),
                    rate);
            }

            chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 100;
            chart.ChartAreas[0].AxisY.Title = "Conversion Rate (%)";
        }

        private static void BuildClientValue(
            Chart chart, DataTable data)
        {
            AddSeries(
                chart, "Payments Received",
                SeriesChartType.Bar, MediumGreen);

            var topClients = data.AsEnumerable()
                .OrderByDescending(
                    row => Number(row, "TotalAmountPaid"))
                .Take(10);

            foreach (DataRow row in topClients)
            {
                chart.Series["Payments Received"].Points.AddXY(
                    StringValue(row, "Client"),
                    Number(row, "TotalAmountPaid"));
            }

            chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chart.ChartAreas[0].AxisY.Title =
                "Total Payments Received (R)";
        }
    }
}
