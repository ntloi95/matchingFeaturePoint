using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FindFeaturePoints
{
    public class point3D
    {
        public double x, y, z;
        public double distanceWithPoint(point3D p)
        {
            return Math.Sqrt(Math.Pow(x-p.x, 2) + Math.Pow(y-p.y, 2) + Math.Pow(z-p.z, 2));
        }
        public point3D(){}
        public point3D(double X, double Y, double Z)
        {
            x = X; y = Y; z = Z;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            point3D[] featurePoints = new point3D[2];
            featurePoints[0] = new point3D(0, 0, 0);
            featurePoints[1] = new point3D(0.1, 0.1, 0.1);

            String fileName = "obj.txt";
            point3D[] matchedPoints = findReplace(fileName, featurePoints);
            foreach(point3D p in matchedPoints)
            {
                Console.WriteLine("{0}, {1}, {2}", p.x, p.y, p.z);
            }
        }

        static public point3D[] findReplace(String fileName, point3D[] featurePoints)
        {
            ArrayList vertices = new ArrayList();
            StreamReader cin = new StreamReader(fileName);
            if (cin != null)
            {
                String line;
                point3D[] result = new point3D[featurePoints.Length];
                while (true)
                {
                    point3D point = new point3D();
                    line = cin.ReadLine();
                    if (line == null)
                        break;
                    if (line[0] == 'v' && line[1] == ' ')
                    {
                        String[] lines = line.Split(' ');
                        point.x = int.Parse(lines[1]);
                        point.y = int.Parse(lines[2]);
                        point.z = int.Parse(lines[3]);

                        vertices.Add(point);
                    }
                }

                int i = 0;
                foreach (point3D p in featurePoints)
                {
                    point3D theBest = (point3D) vertices[0];
                    double minDistance = p.distanceWithPoint((point3D)vertices[0]);
                    foreach (point3D v in vertices)
                    {
                        double dist = p.distanceWithPoint((point3D)v);
                        if (dist < minDistance)
                        {
                            theBest = v;
                            minDistance = dist;
                        }
                    }
                    vertices.Remove(theBest);
                    result[i++] = theBest;
                }
                return result;
            }
            else
            {
                Console.WriteLine("Find not found");
                return null;
            }
        }
    }
}
