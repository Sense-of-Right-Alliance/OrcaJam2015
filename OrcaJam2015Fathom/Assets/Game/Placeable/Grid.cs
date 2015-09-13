using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// ballbusters: legend of the silver surfer
public class Grid : MonoBehaviour
{
    public GameObject TrackPrefab;
    public GameObject OpenSpacePrefab;
    public GameObject MagicianPrefab;
    public GameObject EyePrefab;
    public GameObject FistPrefab;
    public GameObject FootPrefab;
    public GameObject TeethPrefab;

    protected int width;
    protected int height;

    public GameObject[,] Elements;

    public void InitializeGridFromFile(string filepath)
    {
        var lines = File.ReadAllLines(filepath);
        var rows = lines.Skip(2).Select(u => u.Substring(2).Replace(" ", string.Empty).ToCharArray());

        height = rows.Count();
        width = rows.ElementAt(0).Length;

        Elements = new GameObject[height, width];

        int rowIndex = 0;
        foreach (var row in rows)
        {
            int colIndex = 0;
            foreach (var character in row)
            {
                GameObject element;
                Placeable placeable;
                Track track;
                Vector2 position = new Vector2((colIndex - height / (float)2) / (float)2, -(rowIndex - width / (float)2) / (float)2);
                switch (character)
                {
                    case '#':
                        element = (GameObject)Instantiate(TrackPrefab, position, transform.rotation);
                        break;
                    case '1':
                        element = (GameObject)Instantiate(TrackPrefab, position, transform.rotation);
                        track = element.GetComponent<Track>();
                        track.SpawnPosition = 1;
                        break;
                    case '2':
                        element = (GameObject)Instantiate(TrackPrefab, position, transform.rotation);
                        track = element.GetComponent<Track>();
                        track.SpawnPosition = 2;
                        break;
                    case '3':
                        element = (GameObject)Instantiate(TrackPrefab, position, transform.rotation);
                        track = element.GetComponent<Track>();
                        track.SpawnPosition = 3;
                        break;
                    case '4':
                        element = (GameObject)Instantiate(TrackPrefab, position, transform.rotation);
                        track = element.GetComponent<Track>();
                        track.SpawnPosition = 4;
                        break;
                    case 'M':
                        element = (GameObject)Instantiate(MagicianPrefab, position, transform.rotation);
                        break;
                    case 'S':
                        element = (GameObject)Instantiate(FootPrefab, position, transform.rotation);
                        break;
                    case 'W':
                        element = (GameObject)Instantiate(TeethPrefab, position, transform.rotation);
                        break;
                    case 'L':
                        element = (GameObject)Instantiate(EyePrefab, position, transform.rotation);
                        break;
                    case 'D':
                        element = (GameObject)Instantiate(FistPrefab, position, transform.rotation);
                        break;
                    case '.':
                    default:
                        element = (GameObject)Instantiate(OpenSpacePrefab, position, transform.rotation);
                        break;
                }
                placeable = element.GetComponent<Placeable>();
                placeable.XPosition = colIndex;
                placeable.YPosition = rowIndex;
                placeable.Grid = this;
                placeable.transform.parent = transform;

                Elements[rowIndex, colIndex] = element;
                colIndex++;
            }
            rowIndex++;
        }
        InitializeTrackTypes();
        //PrintTrack();
    }

    protected void InitializeTrackTypes()
    {
        foreach (var track in Elements.OfType<GameObject>().Select(u => u.GetComponent<Placeable>()).OfType<Track>().Where(u => u != null))
        {
            var north = track.GetNeighbour(Direction.North) == null ? false : track.GetNeighbour(Direction.North).IsTrack();
            var east = track.GetNeighbour(Direction.East) == null ? false : track.GetNeighbour(Direction.East).IsTrack();
            var south = track.GetNeighbour(Direction.South) == null ? false : track.GetNeighbour(Direction.South).IsTrack();
            var west = track.GetNeighbour(Direction.West) == null ? false : track.GetNeighbour(Direction.West).IsTrack();

            if (north && east && south && west)
            {
                track.TrackType = TrackType.FourWayIntersection;
                track.FacingDirection = Direction.North;
            }
            else if (!north && east && south && west)
            {
                track.TrackType = TrackType.TeeIntersection;
                track.FacingDirection = Direction.North;
            }
            else if (north && !east && south && west)
            {
                track.TrackType = TrackType.TeeIntersection;
                track.FacingDirection = Direction.East;
            }
            else if (north && east && !south && west)
            {
                track.TrackType = TrackType.TeeIntersection;
                track.FacingDirection = Direction.South;
            }
            else if (north && east && south && !west)
            {
                track.TrackType = TrackType.TeeIntersection;
                track.FacingDirection = Direction.West;
            }
            else if (!north && east && south && !west)
            {
                track.TrackType = TrackType.Turn;
                track.FacingDirection = Direction.North;
            }
            else if (!north && !east && south && west)
            {
                track.TrackType = TrackType.Turn;
                track.FacingDirection = Direction.East;
            }
            else if (north && !east && !south && west)
            {
                track.TrackType = TrackType.Turn;
                track.FacingDirection = Direction.South;
            }
            else if (north && east && !south && !west)
            {
                track.TrackType = TrackType.Turn;
                track.FacingDirection = Direction.West;
            }
            else if (north && !east && south && !west)
            {
                track.TrackType = TrackType.Straight;
                track.FacingDirection = Direction.North;
            }
            else if (!north && east && !south && west)
            {
                track.TrackType = TrackType.Straight;
                track.FacingDirection = Direction.East;
            }
            else if (!north && !east && south && !west)
            {
                track.TrackType = TrackType.DeadEnd;
                track.FacingDirection = Direction.North;
            }
            else if (!north && !east && !south && west)
            {
                track.TrackType = TrackType.DeadEnd;
                track.FacingDirection = Direction.East;
            }
            else if (north && !east && !south && !west)
            {
                track.TrackType = TrackType.DeadEnd;
                track.FacingDirection = Direction.South;
            }
            else if (!north && east && !south && !west)
            {
                track.TrackType = TrackType.DeadEnd;
                track.FacingDirection = Direction.West;
            }
            else if (!north && !east && !south && !west)
            {
                throw new System.Exception(string.Format("{0}, {1}: Track sections must be connected to other track sections.", track.XPosition, track.YPosition));
            }
            else
            {
                throw new System.Exception(string.Format("{0}, {1}: Unknown track configuration layout.", track.XPosition, track.YPosition));
            }
        }
    }

    protected void PrintTrack()
    {
        string output = "";
        for (int row = 0; row < height; row++)
        {
            string line = "";
            for (int col = 0; col < width; col++)
            {
                var placeable = GetAtPosition(col, row);
                line += placeable.IsTrack() ? "#" : ".";
            }
            output += line + System.Environment.NewLine;
        }
        Debug.Log(output);
    }

    public Placeable GetAtPosition(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;

        return Elements[y, x].GetComponent<Placeable>();
    }

    public IEnumerable<Track> GetSpawnPositions()
    {
        return Elements
            .OfType<GameObject>()
            .Select(u => u.GetComponent<Placeable>())
            .OfType<Track>()
            .Where(u => u != null && u.SpawnPosition != null)
            .OrderBy(u => u.SpawnPosition);
    }
}
