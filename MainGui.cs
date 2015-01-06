using DwarvenRealms.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace DwarvenRealms
{
    public partial class MainGui : Form
    {
        MapCrafter mc = new MapCrafter();
        int totalChunks, doneChunks;
        public MainGui()
        {
            InitializeComponent();
            minecraftSaveTextBox.Text = Settings.Default.outputPath;
            elevationMapPathTextBox.Text = Settings.Default.elevationMapPath;
            elevationWaterMapPathTextBox.Text = Settings.Default.elevationWaterMapPath;
            biomeMapPathTextBox.Text = Settings.Default.biomeMapPath;
            temperatureMapPathTextBox.Text = Settings.Default.vegetationMapPath;
            vegetationMapPathTextBox.Text = Settings.Default.vegetationMapPath;
            volcanismMapPathTextBox.Text = Settings.Default.vegetationMapPath;
            evilMapPathTextBox.Text = Settings.Default.vegetationMapPath;
            borderNorthInput.Value = Settings.Default.borderNorth;
            borderSouthInput.Value = Settings.Default.borderSouth;
            borderWestInput.Value = Settings.Default.borderWest;
            borderEastInput.Value = Settings.Default.borderEast;
            centerXInput.Value = Settings.Default.mapCenterX;
            centerYInput.Value = Settings.Default.mapCenterY;
            blocksPerTileInput.Value = Settings.Default.blocksPerEmbarkTile;
            caveCoverageInput.Value = Settings.Default.cavePercentage;
            caveHeightInput.Value = (decimal)Settings.Default.caveHeight;
            caveWidthInput.Value = (decimal)Settings.Default.caveScale;
            levelNameTextBox.Text = Settings.Default.levelName;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\saves";
            minecraftSaveSelector.SelectedPath = path;
        }

        private void MapGenerationWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MapCrafter craft = e.Argument as MapCrafter;
            mapGenerationWorker.ReportProgress(0,"Initializing Minecraft Realm...");
            craft.initializeMinecraftWorld();
            mapGenerationWorker.ReportProgress(0,"Loading Dorf Maps...");
            craft.loadDwarfMaps();
            totalChunks = (MapCrafter.getChunkFinishX() - MapCrafter.getChunkStartX()) *
                 (MapCrafter.getChunkFinishY() - MapCrafter.getChunkStartY());
            doneChunks = 0;
            Stopwatch watch = Stopwatch.StartNew();
            Stopwatch unitTime = new Stopwatch();
            for (int xi = MapCrafter.getChunkStartX(); xi < MapCrafter.getChunkFinishX(); xi++)
            {
                for (int zi = MapCrafter.getChunkStartY(); zi < MapCrafter.getChunkFinishY(); zi++)
                {
                    if (mapGenerationWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    unitTime.Restart();
                    craft.generateSingleChunk(xi, zi);
                    doneChunks++;
                    TimeSpan elapsedTime = watch.Elapsed;
                    TimeSpan remainingTime = TimeSpan.FromTicks(elapsedTime.Ticks / doneChunks * (totalChunks - doneChunks));
                    double speed = 1000 / unitTime.ElapsedMilliseconds;
                    mapGenerationWorker.ReportProgress((doneChunks * 1000) / totalChunks, "Done " + doneChunks + " out of " + totalChunks + " Minecraft chunks at " + speed + " chunks per second. " + remainingTime.ToString(@"hh\:mm\:ss") + " left.");
                }
                if (mapGenerationWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
            craft.saveMinecraftWorld();
        }

        private void UpdateAreaOutput()
        {
            double area = (Settings.Default.borderSouth - Settings.Default.borderNorth) * (Settings.Default.borderEast - Settings.Default.borderWest)
                * Settings.Default.blocksPerEmbarkTile * Settings.Default.blocksPerEmbarkTile / 1000000.0;
            areaOutputText.Text = "= " + area.ToString("F2") + "km²";
        }

        private void MapGenerationWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MapGenerationProgressBar.Value = e.ProgressPercentage;
            MapGenerationProgressLable.Text = e.UserState as string;
        }

        private void MapGenerationWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MapGenerationStartButton.Enabled = true;
            MapGenerationAbortButton.Enabled = false;
            if (e.Cancelled)
                MapGenerationProgressLable.Text = "Aborted map generation.";
            else
                MapGenerationProgressLable.Text = "Done! Hopefully.";
        }

        private void MapGenerationStartButton_Click(object sender, EventArgs e)
        {
            MapGenerationStartButton.Enabled = false;
            MapGenerationAbortButton.Enabled = true;
            mapGenerationWorker.RunWorkerAsync(mc);
        }

        private void MapGenerationAbortButton_Click(object sender, EventArgs e)
        {
            mapGenerationWorker.CancelAsync();
        }

        private void minecraftSaveButton_Click(object sender, EventArgs e)
        {
            DialogResult result = minecraftSaveSelector.ShowDialog();
            if(result == DialogResult.OK)
            {
                minecraftSaveTextBox.Text = minecraftSaveSelector.SelectedPath;
            }
        }

        private void minecraftSaveTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(minecraftSaveTextBox.Text) || minecraftSaveTextBox.Text.Trim().Length == 0)
                label4.BackColor = incompleteFormColor;
            else label4.BackColor = completeFormColor;
            Settings.Default.outputPath = minecraftSaveTextBox.Text;
            Console.WriteLine("Output path changed to " + Settings.Default.outputPath);
        }

        private void elevationMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(elevationMapPathTextBox.Text) || elevationMapPathTextBox.Text.Trim().Length == 0) 
                elevationMapLabel.BackColor = incompleteFormColor;
            else elevationMapLabel.BackColor = completeFormColor;
            Settings.Default.elevationMapPath = elevationMapPathTextBox.Text;
            Console.WriteLine("Elevation map path changed to " + Settings.Default.elevationMapPath);
        }

        private void elevationWaterMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(elevationWaterMapPathTextBox.Text) || elevationWaterMapPathTextBox.Text.Trim().Length == 0) 
                elevationWaterMapLabel.BackColor = incompleteFormColor;
            else elevationWaterMapLabel.BackColor = completeFormColor;
            Settings.Default.elevationWaterMapPath = elevationWaterMapPathTextBox.Text;
            Console.WriteLine("Elevation water map path changed to " + Settings.Default.elevationWaterMapPath);
        }

        private void biomeMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(biomeMapPathTextBox.Text) || biomeMapPathTextBox.Text.Trim().Length == 0) 
                biomeMapLabel.BackColor = incompleteFormColor;
            else biomeMapLabel.BackColor = completeFormColor;
            Settings.Default.biomeMapPath = biomeMapPathTextBox.Text;
            Console.WriteLine("Biome map path changed to " + Settings.Default.biomeMapPath);
        }

        private void temperatureMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(temperatureMapPathTextBox.Text) || temperatureMapPathTextBox.Text.Trim().Length == 0) 
                temperatureMapLabel.BackColor = incompleteFormColor;
            else temperatureMapLabel.BackColor = completeFormColor;
            Settings.Default.temperatureMapPath = temperatureMapPathTextBox.Text;
            Console.WriteLine("Temperature map path changed to " + Settings.Default.temperatureMapPath);
        }

        private void vegetationMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(vegetationMapPathTextBox.Text) || vegetationMapPathTextBox.Text.Trim().Length == 0) 
                vegetationMapLabel.BackColor = incompleteFormColor;
            else vegetationMapLabel.BackColor = completeFormColor;
            Settings.Default.vegetationMapPath = vegetationMapPathTextBox.Text;
            Console.WriteLine("Vegetation map path changed to " + Settings.Default.vegetationMapPath);
        }

        private void volcanismMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(volcanismMapPathTextBox.Text) || volcanismMapPathTextBox.Text.Trim().Length == 0) 
                volcanismMapLabel.BackColor = incompleteFormColor;
            else volcanismMapLabel.BackColor = completeFormColor;
            Settings.Default.volcanismMapPath = volcanismMapPathTextBox.Text;
            Console.WriteLine("Volcanism map path changed to " + Settings.Default.volcanismMapPath);
        }

        private void evilMapPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(evilMapPathTextBox.Text) || evilMapPathTextBox.Text.Trim().Length == 0) 
                evilMapLabel.BackColor = incompleteFormColor;
            else evilMapLabel.BackColor = completeFormColor;
            Settings.Default.evilMapPath = evilMapPathTextBox.Text;
            Console.WriteLine("Evil map path changed to " + Settings.Default.evilMapPath);
        }

        private void elevationMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = elevationMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                elevationMapPathTextBox.Text = elevationMapFileDialog.FileName;
            }
        }

        private void elevationWaterMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = elevationWaterMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                elevationWaterMapPathTextBox.Text = elevationWaterMapFileDialog.FileName;
            }
        }

        private void biomeMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = biomeMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
               biomeMapPathTextBox.Text = biomeMapFileDialog.FileName;
            }
        }

        private void temperatureMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = temperatureMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                temperatureMapPathTextBox.Text = temperatureMapFileDialog.FileName;
            }
        }

        private void vegetationMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = vegetationMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                vegetationMapPathTextBox.Text = vegetationMapFileDialog.FileName;
            }
        }

        private void volcanismMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = volcanismMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                volcanismMapPathTextBox.Text = volcanismMapFileDialog.FileName;
            }
        }

        private void evilMapLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = evilMapFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                evilMapPathTextBox.Text = evilMapFileDialog.FileName;
            }
        }

        private void borderNorthInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.borderNorth = (int)borderNorthInput.Value;
            borderSouthInput.Minimum = borderNorthInput.Value + 1;
            UpdateAreaOutput();
        }

        private void borderEastInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.borderEast = (int)borderEastInput.Value;
            borderWestInput.Maximum = borderEastInput.Value - 1;
            UpdateAreaOutput();
        }

        private void borderSouthInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.borderSouth = (int)borderSouthInput.Value;
            borderNorthInput.Maximum = borderSouthInput.Value - 1;
            UpdateAreaOutput();
        }

        private void borderWestInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.borderWest = (int)borderWestInput.Value;
            borderEastInput.Minimum = borderWestInput.Value + 1;
            UpdateAreaOutput();
        }

        private void recenterButton_Click(object sender, EventArgs e)
        {
            centerXInput.Value = (borderWestInput.Value + borderEastInput.Value) / 2;
            centerYInput.Value = (borderNorthInput.Value + borderSouthInput.Value) / 2;
        }

        private void centerXInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.mapCenterX = (int)centerXInput.Value;
        }

        private void centerYInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.mapCenterY = (int)centerYInput.Value;
        }

        private void blocksPerTileInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.blocksPerEmbarkTile = (int)blocksPerTileInput.Value;
            UpdateAreaOutput();
        }

        private void caveHeightInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.caveHeight = (double)caveHeightInput.Value;
        }

        private void caveWidthInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.caveScale = (double)caveWidthInput.Value;
        }

        private void caveCoverageInput_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.cavePercentage = (int)caveCoverageInput.Value;
        }

        private void levelNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.levelName = levelNameTextBox.Text;
        }

    }
}
