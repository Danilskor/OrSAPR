using System;
using KompasConnector;
using BottleParameters;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;



namespace BottleBuilder
{
    /// <summary>
    /// Class for build laboratory bottle 
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Variable for connecting with Kompas
        /// </summary>
        private Konnector _connector;

        /// <summary>
        /// Bottle parameters
        /// </summary>
        private Parameters _parameters;
        
        /// <summary>
        /// Variable pointing to sketchPoint
        /// </summary>
        private ksSketchDefinition  _sketch;

        /// <summary>
        /// Method for building bottle
        /// </summary>
        /// <param name="konnector">Variable for connecting with Kompas</param>
        /// <param name="parameters">List of parameters</param>
        public void BuildBottle(Konnector konnector, Parameters parameters)
        {
            _connector = konnector;
            _connector.GetNewPart();
            _parameters = parameters;

            BuildBottleBase();

            if (_parameters.IsBottleStraight)
            {
                BuildStraightBottleTop();
                BuildStraightBottleHandle();
            }
            else
            {
                BuildBottleTop();
                BuildHandle();
            }

        }

        /// <summary>
        /// Method for building handle
        /// </summary>
        private void BuildHandle()
        {
            //TODO: RSDN
            var angle = Math.Atan((_parameters.CoverRadius / 2 - 
                                   _parameters.Width / 8 * 3 - 
                                   _parameters.WallThickness) / 
                                  (_parameters.Height / 4 + 
                                   _parameters.WallThickness)) / Math.PI * 180;

            var pressThickness = _parameters.Height / 4 + 
                                 _parameters.WallThickness;
            PressOutSketch(_sketch, pressThickness, true, angle);

            var chamferPoint = new Point3D
            {
                X = _parameters.CoverRadius / 2 - _parameters.WallThickness,
                Y = 0,
                Z = _parameters.Height + _parameters.WallThickness * 2
            };
            CreateChamfer(chamferPoint,  
                _parameters.WallThickness, _parameters.WallThickness);

            var sketchPoint = new Point3D()
            {
                X = 0,
                Y = 0,
                Z = _parameters.Height + _parameters.WallThickness * 2
            };
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            var circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            var circleRadius = _parameters.HandleBaseRadius / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            pressThickness = _parameters.HandleLength;
            PressOutSketch(_sketch, pressThickness, true, 0);

            sketchPoint = new Point3D()
            {
                X = 0,
                Y = 0,
                Z = _parameters.Height +
                    _parameters.WallThickness * 2 +
                    _parameters.HandleLength
            };
            
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            circleRadius = _parameters.HandleRadius / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);

            pressThickness = _parameters.HandleRadius;
            PressOutSketch(_sketch, pressThickness, true, 0);

            var filletPoint = new Point3D
            {
                X = _parameters.HandleRadius / 2,
                Y = 0,
                Z = _parameters.Height + _parameters.WallThickness * 2 +
                    _parameters.HandleLength + _parameters.HandleRadius / 2
            };

            var filletRadius = _parameters.HandleRadius / 2;
            CreateFaceFillet(filletPoint, filletRadius);

            filletPoint = new Point3D
            {
                X = _parameters.HandleBaseRadius / 2,
                Y = 0,
                Z = _parameters.Height +
                    _parameters.WallThickness * 2 +
                    _parameters.HandleLength / 2
            };
            filletRadius = _parameters.HandleBaseRadius / 4;
            CreateFaceFillet(filletPoint, filletRadius);
        }

        /// <summary>
        /// Method for building handle of the straight bottle
        /// </summary>
        private void BuildStraightBottleHandle()
        {
            //TODO: RSDN
            var pressThickness = _parameters.Height / 4 + 
                                 _parameters.WallThickness * 2;
            var angle = 0;
            PressOutSketch(_sketch, pressThickness, true, angle);

            var sketchPoint = new Point3D
            {
                X = _parameters.Width / 2 - _parameters.WallThickness / 2, Y = 0, Z = _parameters.Height
            };
            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, false, sketchPoint);

            var chamferPoint = new Point3D
            {
                X = _parameters.Width / 2, 
                Y = 0, 
                Z = _parameters.Height
            };
            CreateChamfer(chamferPoint, _parameters.WallThickness, _parameters.WallThickness);

            var circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            var circleRadius = _parameters.Width / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);

            pressThickness = _parameters.WallThickness * 2;
            angle = 0;
            PressOutSketch(_sketch, pressThickness, true, angle);

            var filletPoint = new Point3D
            {
                X = _parameters.Width / 2,
                Y = 0,
                Z = _parameters.Height +
                    _parameters.WallThickness
            };
            var filletRadius = _parameters.WallThickness;
            CreateFaceFillet(filletPoint, filletRadius);

            sketchPoint = new Point3D
            {
                X = 0, 
                Y = 0, 
                Z = _parameters.Height + _parameters.WallThickness * 2
            };
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            circleRadius = _parameters.HandleBaseRadius / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            pressThickness = _parameters.HandleLength;
            PressOutSketch(_sketch, pressThickness, true, 0);

            sketchPoint = new Point3D
            {
                X = 0,
                Y = 0,
                Z = _parameters.Height +
                    _parameters.WallThickness * 2 +
                    _parameters.HandleLength
            };
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre = new Point2D
            {
                X = 0,
                Y = 0
            };
            circleRadius = _parameters.HandleRadius / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);

            pressThickness = _parameters.HandleRadius;
            PressOutSketch(_sketch, pressThickness, true, 0);

            filletPoint = new Point3D
            {
                X = _parameters.HandleRadius / 2,
                Y = 0,
                Z = _parameters.Height + _parameters.WallThickness * 2 +
                    _parameters.HandleLength + _parameters.HandleRadius / 2
            };

            filletRadius = _parameters.HandleRadius / 2;
            CreateFaceFillet(filletPoint, filletRadius);

            filletPoint = new Point3D
            {
                X = _parameters.HandleBaseRadius / 2,
                Y = 0,
                Z = _parameters.Height +
                    _parameters.WallThickness * 2 +
                    _parameters.HandleLength / 2
            };
            filletRadius = _parameters.HandleBaseRadius / 4;
            CreateFaceFillet(filletPoint, filletRadius);
        }

        /// <summary>
        /// Method for building base of the bottle
        /// </summary>
        private void BuildBottleBase()
        {
            var sketchPoint = new Point3D
            {
                X = 0,
                Y = 0,
                Z = 0
            };

            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, true, sketchPoint);

            var circleCentre = new Point2D
            {
                X = 0,
                Y = 0
            };
            double circleRadius = _parameters.Width / 2 - _parameters.WallThickness;
            CreateCircle(_sketch, circleCentre, circleRadius);

            var pressThickness = _parameters.WallThickness;
            double pressHeight = _parameters.Height / 4 * 3;
            PressOutSketchThickness(_sketch, pressHeight, pressThickness, true, 0);

            pressThickness = _parameters.WallThickness;
            PressOutSketch(_sketch, pressThickness, false, 0);

            var filletPoint = new Point3D
            {
                X = _parameters.Width / 2,
                Y = 0,
                Z = 0
            };
            CreateEdgeFillet(filletPoint, _parameters.WallThickness);

            sketchPoint = new Point3D
            {
                X = _parameters.Width / 2 - 2,
                Y = 0,
                Z = _parameters.Height / 4 * 3
            };
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);
        }



        /// <summary>
        /// Method for building top of the bottle
        /// </summary>
        private void BuildBottleTop()
        {
            //TODO: RSDN
            var circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            var circleRadius = _parameters.Width / 8 * 3;
            CreateCircle(_sketch, circleCentre, circleRadius);

            circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            circleRadius = _parameters.Width / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            var pressThickness = _parameters.WallThickness;
            PressOutSketch(_sketch, pressThickness, false, 0);

            var filletPoint = new Point3D
            {
                X = _parameters.Width / 2, 
                Y = 0, 
                Z = _parameters.Height / 4 * 3 + _parameters.WallThickness
            };
            CreateEdgeFillet(filletPoint, _parameters.WallThickness);

            var sketchPoint = new Point3D
            {
                X = _parameters.Width / 2 - _parameters.WallThickness - 2,
                Y = 0,
                Z = _parameters.Height / 4 * 3 + _parameters.WallThickness
            };
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            circleRadius = _parameters.Width / 8 * 3;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            double angle = (Math.Atan(((_parameters.CoverRadius / 2 -
                                        _parameters.Width / 8 * 3 -
                                        _parameters.WallThickness) /
                                       (_parameters.Height / 4))) / Math.PI * 180);

            var pressHeight = _parameters.Height / 4;
            pressThickness = _parameters.WallThickness;
            PressOutSketchThickness(_sketch, pressHeight, pressThickness, true, angle);

            filletPoint = new Point3D
            {
                X = _parameters.CoverRadius / 2, 
                Y = 0, 
                Z = _parameters.Height + _parameters.WallThickness
            };
            var filletRadius = _parameters.WallThickness / 2;
            CreateEdgeFillet(filletPoint, filletRadius);

            filletPoint = new Point3D
            {
                X = _parameters.CoverRadius / 2 - _parameters.WallThickness,
                Y = 0,
                Z = _parameters.Height + _parameters.WallThickness
            };
            filletRadius = _parameters.WallThickness / 2;
            CreateEdgeFillet(filletPoint, filletRadius);
        }

        /// <summary>
        /// Method for building top of the straight bottle
        /// </summary>
        private void BuildStraightBottleTop()
        {
            //TODO: RSDN
            
            

            var sketchPoint = new Point3D
            {
                X = _parameters.Width / 2 - _parameters.WallThickness / 2, Y = 0, Z = _parameters.Height / 4 * 3
            };
            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, false, sketchPoint);

            var circleCentre = new Point2D
            {
                X = 0, 
                Y = 0
            };
            var circleRadius = _parameters.Width / 2 - _parameters.WallThickness;
            CreateCircle(_sketch, circleCentre, circleRadius);

            var pressHight = _parameters.Height / 4 * 1;
            var pressThickness = _parameters.WallThickness;
            var angle = 0;
            PressOutSketchThickness(_sketch, pressHight, pressThickness, true, angle);
        }
        
        /// <summary>
        /// Method for creating sketch
        /// </summary>
        /// <param name="planeType">Type of the plane</param>
        /// <param name="isFirstSketch"></param>
        /// <param name="point">The point where the sketch will be created</param>
        /// <returns>Definition of the sketchPoint</returns>
        private ksSketchDefinition CreateSketch(Obj3dType planeType, 
            bool isFirstSketch, Point3D point)
        {
            ksEntity plane = (ksEntity)_connector
                .KsPart
                .GetDefaultEntity((short)planeType);

            var sketchPoint = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_sketch);

            var sketchDefinition = (ksSketchDefinition)sketchPoint.GetDefinition();
            if (!isFirstSketch)
            {
                ksEntityCollection iCollection = 
                    _connector.KsPart.EntityCollection((short)Obj3dType.o3d_face);
                iCollection.SelectByPoint(point.X, point.Y, point.Z);
                plane = iCollection.First();
            }
            sketchDefinition.SetPlane(plane);

            sketchPoint.Create();
            return sketchDefinition;
        }

        /// <summary>
        /// Method for creating circle on sketchPoint
        /// </summary>
        /// <param name="sketchPoint">sketchPoint</param>
        /// <param name="centre">Coordinates of centre circle</param>
        /// <param name="radius"> Radius of the circle</param>
        private void CreateCircle(ksSketchDefinition sketchPoint, Point2D centre, double radius)
        {
            var circle = (ksCircleParam)_connector
                .Kompas
                .GetParamStruct((short)StructType2DEnum.ko_CircleParam);

            circle.style = 1;
            var doc2D = (ksDocument2D)_sketch.BeginEdit();
            doc2D.ksCircle(centre.X, centre.Y, radius, circle.style);
            sketchPoint.EndEdit();
        }

        /// <summary>
        /// Extrude from sketchPoint
        /// </summary>
        /// <param name="sketchPoint">sketchPoint</param>
        /// <param name="thickness">Thickness of extrude</param>
        /// <param name="side">Side</param>
        /// <param name="draftValue">Draft value</param>
        private void PressOutSketch(
            ksSketchDefinition sketchPoint,
            double thickness, bool side, double draftValue)
        {
            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_bossExtrusion);

            var extrusionDefinition = (ksBossExtrusionDefinition)extrusionEntity
                .GetDefinition();
            
            extrusionDefinition.SetSideParam(side, 0, thickness);

            extrusionDefinition.SetSketch(sketchPoint);
            ExtrusionParam extrusionParam =  extrusionDefinition.ExtrusionParam();
            extrusionParam.depthNormal = thickness;
            extrusionParam.draftValueNormal = draftValue;

            extrusionEntity.Create();
        }

        /// <summary>
        /// Extrude a thin-walled feature from sketchPoint
        /// </summary>
        /// <param name="sketchPoint">sketchPoint</param>
        /// <param name="height">Height of extrude</param>
        /// <param name="wallThickness">Thickness of extrude</param>
        /// <param name="side">Side</param>
        /// <param name="draftValue">Draft value</param>
        private void PressOutSketchThickness( ksSketchDefinition sketchPoint,
            double height, double wallThickness, bool side, double draftValue)
        {

            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_baseExtrusion);
            
            var extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity
                .GetDefinition();
            
            extrusionDefinition.SetSideParam(side, 0, height);
            extrusionDefinition.SetThinParam(true, 0, wallThickness);
            
            
            extrusionDefinition.SetSketch(sketchPoint);
            ExtrusionParam extrusionParam = extrusionDefinition.ExtrusionParam();
            extrusionParam.draftValueNormal = draftValue;
            extrusionEntity.Create();
        }

        /// <summary>
        /// Add filletPoint in filletPoint array
        /// </summary>
        /// <param name="radius">filletPoint radius</param>
        /// <returns name="filletEntity">filletPoint entity</returns>
        private ksEntity AddFillet(double radius)
        {
            var filletEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_fillet);

            var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();

            filletDefinition.radius = radius;

            filletDefinition.tangent = true;
            
            return filletEntity;
        }

        /// <summary>
        /// Create сhamfer
        /// </summary>
        /// <param name="point">Coordinates pointing to a face to be сhamfered</param>
        /// <param name="distance1"></param>
        /// <param name="distance2"></param>
        private void CreateChamfer(Point3D point, double distance1, double distance2)
        {
            var chamferEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_chamfer);

            var chamferDefinition = (ksChamferDefinition)chamferEntity.GetDefinition();
            chamferDefinition.SetChamferParam(true, distance1, distance2);

            ksEntityCollection iArray = chamferDefinition.array();
            ksEntityCollection iCollection = _connector.KsPart.EntityCollection((short)Obj3dType.o3d_edge);

            iCollection.SelectByPoint(point.X, point.Y, point.Z);

            var iEdge = iCollection.Last();
            iArray.Add(iEdge);

            chamferEntity.Create();
        }

        /// <summary>
        /// Create filletPoint of the face
        /// </summary>
        /// <param name="point">Coordinates pointing to a face or surface to be filleted</param>
        /// <param name="radius">filletPoint radius</param>
        private void CreateFaceFillet(Point3D point, double radius)
        {
            var filletEntity = AddFillet(radius);
            var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();

            ksEntityCollection iArray = filletDefinition.array();
            ksEntityCollection iCollection = _connector.KsPart.EntityCollection((short)Obj3dType.o3d_face);

            iCollection.SelectByPoint(point.X, point.Y, point.Z);
            var iFace = iCollection.First();
            iArray.Add(iFace);

            filletEntity.Create();
        }

        /// <summary>
        /// Create filletPoint of the edge
        /// </summary>
        /// <param name="point">Coordinates pointing to a face or surface to be filleted</param>
        /// <param name="radius">filletPoint radius</param>
        private void CreateEdgeFillet(Point3D point, double radius)
        {
            ksEntity filletEntity = AddFillet(radius);
            var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();

            ksEntityCollection iArray = filletDefinition.array();
            ksEntityCollection iCollection = _connector.KsPart.EntityCollection((short)Obj3dType.o3d_edge);

            iCollection.SelectByPoint(point.X, point.Y, point.Z);
            var iEdge = iCollection.Last();
            iArray.Add(iEdge);

            filletEntity.Create();
        }
    }
}
