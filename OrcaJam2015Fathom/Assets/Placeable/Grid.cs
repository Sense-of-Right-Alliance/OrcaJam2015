using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// ballbusters: legend of the silver surfer
public class Grid : MonoBehaviour
{
    public Placeable Track;
    public Placeable OpenSpace;
    
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
                switch (character)
                {
                    case '#':
                        element = (GameObject)Instantiate(Track, transform.position, transform.rotation);
                        placeable = element.GetComponent<Track>();
                        break;
                    case 'M':
                    case 'S':
                    case 'W':
                    case 'L':
                    case 'D':
                    case '.':
                    default:
                        element = (GameObject)Instantiate(OpenSpace, transform.position, transform.rotation);
                        placeable = element.GetComponent<OpenSpace>();
                        break;
                }
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
                throw new System.Exception("Track sections must be connected to other track sections.");
            }
            else
            {
                throw new System.Exception("Unknown track configuration layout.");
            }
        }
    }

    public Placeable GetAtPosition(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;

        return Elements[x, y].GetComponent<Placeable>();
    }
}
