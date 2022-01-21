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
            var sketchPoint = new Point3D();
            var filletPoint = new Point3D();
            var chamferPoint = new Point3D();
            double filletRadius;
            var circleCentre = new Point2D();
            double circleRadius;
            

            //TODO: RSDN
            var angle = Math.Atan((_parameters.CoverRadius.ParameterValue / 2 - _parameters.Width.ParameterValue / 8 * 3 - 
                                   _parameters.WallThickness.ParameterValue) / 
                                  (_parameters.Height.ParameterValue / 4 + _parameters.WallThickness.ParameterValue)) / Math.PI * 180;

            var pressThickness = _parameters.Height.ParameterValue / 4 + _parameters.WallThickness.ParameterValue;
            PressOutSketch(_sketch, pressThickness, true, angle);

            chamferPoint.X = _parameters.CoverRadius.ParameterValue / 2 - _parameters.WallThickness.ParameterValue;
            chamferPoint.Y = 0;
            chamferPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue * 2;
            CreateChamfer(chamferPoint,  _parameters.WallThickness.ParameterValue, _parameters.WallThickness.ParameterValue);

            //TODO: Переписать все использования этих полей. Поля удалить. Создать класс/структуру 3D точки.
            sketchPoint.X = 0;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue * 2;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.HandleBaseRadius.ParameterValue / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            pressThickness = _parameters.HandleLength.ParameterValue;
            PressOutSketch(_sketch, pressThickness, true, 0);

            sketchPoint.X = 0;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue + 
                            _parameters.WallThickness.ParameterValue * 2 + 
                            _parameters.HandleLength.ParameterValue;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);


            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.HandleRadius.ParameterValue / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);

            pressThickness = _parameters.HandleRadius.ParameterValue;
            PressOutSketch(_sketch, pressThickness, true, 0);

            filletPoint.X = _parameters.HandleRadius.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue * 2 + 
                       _parameters.HandleLength.ParameterValue + _parameters.HandleRadius.ParameterValue / 2;

            filletRadius = _parameters.HandleRadius.ParameterValue / 2;
            CreateFaceFillet(filletPoint, filletRadius);

            filletPoint.X = _parameters.HandleBaseRadius.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue + 
                            _parameters.WallThickness.ParameterValue * 2 + 
                            _parameters.HandleLength.ParameterValue / 2;
            filletRadius = _parameters.HandleBaseRadius.ParameterValue / 4;
            CreateFaceFillet(filletPoint, filletRadius);
        }

        private void BuildStraightBottleHandle()
        {
            var sketchPoint = new Point3D();
            var filletPoint = new Point3D();
            var chamferPoint = new Point3D();
            double filletRadius;
            var circleCentre = new Point2D();
            double circleRadius;

            var pressThickness = _parameters.Height.ParameterValue / 4 + 
                                 _parameters.WallThickness.ParameterValue * 2;
            var angle = 0;
            PressOutSketch(_sketch, pressThickness, true, angle);

            sketchPoint.X = _parameters.Width.ParameterValue / 2 - _parameters.WallThickness.ParameterValue / 2;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue;
            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, false, sketchPoint);

            chamferPoint.X = _parameters.Width.ParameterValue / 2;
            chamferPoint.Y = 0;
            chamferPoint.Z = _parameters.Height.ParameterValue;
            CreateChamfer(chamferPoint, _parameters.WallThickness.ParameterValue, _parameters.WallThickness.ParameterValue);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.Width.ParameterValue / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);

            pressThickness = _parameters.WallThickness.ParameterValue * 2;
            angle = 0;
            PressOutSketch(_sketch, pressThickness, true, angle);

            filletPoint.X = _parameters.Width.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue +
                            _parameters.WallThickness.ParameterValue;
            filletRadius = _parameters.WallThickness.ParameterValue;
            CreateFaceFillet(filletPoint, filletRadius);

            sketchPoint.X = 0;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue * 2;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.HandleBaseRadius.ParameterValue / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            pressThickness = _parameters.HandleLength.ParameterValue;
            PressOutSketch(_sketch, pressThickness, true, 0);

            sketchPoint.X = 0;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue +
                            _parameters.WallThickness.ParameterValue * 2 +
                            _parameters.HandleLength.ParameterValue;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);


            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.HandleRadius.ParameterValue / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);

            pressThickness = _parameters.HandleRadius.ParameterValue;
            PressOutSketch(_sketch, pressThickness, true, 0);

            filletPoint.X = _parameters.HandleRadius.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue * 2 +
                       _parameters.HandleLength.ParameterValue + _parameters.HandleRadius.ParameterValue / 2;

            filletRadius = _parameters.HandleRadius.ParameterValue / 2;
            CreateFaceFillet(filletPoint, filletRadius);

            filletPoint.X = _parameters.HandleBaseRadius.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue +
                            _parameters.WallThickness.ParameterValue * 2 +
                            _parameters.HandleLength.ParameterValue / 2;
            filletRadius = _parameters.HandleBaseRadius.ParameterValue / 4;
            CreateFaceFillet(filletPoint, filletRadius);
        }

        /// <summary>
        /// Method for building base of the bottle
        /// </summary>
        private void BuildBottleBase()
        {
            Point3D sketchPoint = new Point3D();
            Point3D filletPoint = new Point3D();
            double filletRadius;
            Point2D circleCentre = new Point2D();
            double circleRadius;

            sketchPoint.X = 0;
            sketchPoint.Y = 0;
            sketchPoint.Z = 0;
            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, true, sketchPoint);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.Width.ParameterValue / 2 - _parameters.WallThickness.ParameterValue;
            CreateCircle(_sketch, circleCentre, circleRadius);

            var pressThickness = _parameters.WallThickness.ParameterValue;
            double pressHeight = _parameters.Height.ParameterValue / 4 * 3;
            PressOutSketchThickness(_sketch, pressHeight, pressThickness, true, 0);

            pressThickness = _parameters.WallThickness.ParameterValue;
            PressOutSketch(_sketch, pressThickness, false, 0);

            filletPoint.X = _parameters.Width.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = 0;
            CreateEdgeFillet(filletPoint, _parameters.WallThickness.ParameterValue);

            sketchPoint.X = _parameters.Width.ParameterValue / 2 - 2;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue / 4 * 3;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);
        }



        /// <summary>
        /// Method for building top of the bottle
        /// </summary>
        private void BuildBottleTop()
        {
            Point3D sketchPoint = new Point3D();
            Point3D filletPoint = new Point3D();
            double filletRadius;
            Point2D circleCentre = new Point2D();
            double circleRadius;

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.Width.ParameterValue / 8 * 3;
            CreateCircle(_sketch, circleCentre, circleRadius);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.Width.ParameterValue / 2;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            var pressThickness = _parameters.WallThickness.ParameterValue;
            PressOutSketch(_sketch, pressThickness, false, 0);

            filletPoint.X = _parameters.Width.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue / 4 * 3 + _parameters.WallThickness.ParameterValue;
            CreateEdgeFillet(filletPoint, _parameters.WallThickness.ParameterValue);

            sketchPoint.X = _parameters.Width.ParameterValue / 2 - _parameters.WallThickness.ParameterValue - 2;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue / 4 * 3 + _parameters.WallThickness.ParameterValue;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, sketchPoint);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.Width.ParameterValue / 8 * 3;
            CreateCircle(_sketch, circleCentre, circleRadius);
            _sketch.EndEdit();

            double angle = (Math.Atan(((_parameters.CoverRadius.ParameterValue / 2 -
                                        _parameters.Width.ParameterValue / 8 * 3 -
                                        _parameters.WallThickness.ParameterValue) /
                                       (_parameters.Height.ParameterValue / 4))) / Math.PI * 180);

            var pressHeight = _parameters.Height.ParameterValue / 4;
            pressThickness = _parameters.WallThickness.ParameterValue;
            PressOutSketchThickness(_sketch, pressHeight, pressThickness, true, angle);

            filletPoint.X = _parameters.CoverRadius.ParameterValue / 2;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue;
            filletRadius = _parameters.WallThickness.ParameterValue / 2;
            CreateEdgeFillet(filletPoint, filletRadius);

            filletPoint.X = _parameters.CoverRadius.ParameterValue / 2 - _parameters.WallThickness.ParameterValue;
            filletPoint.Y = 0;
            filletPoint.Z = _parameters.Height.ParameterValue + _parameters.WallThickness.ParameterValue;
            filletRadius = _parameters.WallThickness.ParameterValue / 2;
            CreateEdgeFillet(filletPoint, filletRadius);
            }

        private void BuildStraightBottleTop()
        {
            Point3D sketchPoint = new Point3D();
            Point3D filletPoint = new Point3D();
            double filletRadius;
            Point2D circleCentre = new Point2D();
            double circleRadius;

            sketchPoint.X = _parameters.Width.ParameterValue / 2 - _parameters.WallThickness.ParameterValue / 2;
            sketchPoint.Y = 0;
            sketchPoint.Z = _parameters.Height.ParameterValue / 4 * 3;
            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, false, sketchPoint);

            circleCentre.X = 0;
            circleCentre.Y = 0;
            circleRadius = _parameters.Width.ParameterValue / 2 - _parameters.WallThickness.ParameterValue;
            CreateCircle(_sketch, circleCentre, circleRadius);

            var pressHight = _parameters.Height.ParameterValue / 4 * 1;
            var pressThickness = _parameters.WallThickness.ParameterValue;
            var angle = 0;
            PressOutSketchThickness(_sketch, pressHight, pressThickness, true, angle);
        }

        /// <summary>
        /// Method for creating sketchPoint
        /// </summary>
        /// <param name="planeType">Type of the plane</param>
        /// <param name="isFirstSketch"></param>
        /// <param name="x">Coordinate X pointing to a surface when will be create sketchPoint</param>
        /// <param name="y">Coordinate Y pointing to a surface when will be create sketchPoint</param>
        /// <param name="z">Coordinate Z pointing to a surface when will be create sketchPoint</param>
        /// <returns name=sketchDefinition> Definition of the sketchPoint</returns>
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
        /// <param name="centreX">Coordinate X of centre circle</param>
        /// <param name="centreY">Coordinate Y of centre circle</param>
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
            ksEntity filletEntity = (ksEntity)_connector
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
        /// <param name="x">Coordinate X pointing to a face to be сhamfered</param>
        /// <param name="y">Coordinate Y pointing to a face to be сhamfered</param>
        /// <param name="z">Coordinate Z pointing to a face to be сhamfered</param>
        /// <param name="distance1"></param>
        /// <param name="distance2"></param>
        private void CreateChamfer(Point3D point, double distance1, double distance2)
        {
            ksEntity chamferEntity = (ksEntity)_connector
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
        /// <param name="x">Coordinate X pointing to a face or surface to be filleted</param>
        /// <param name="y">Coordinate Y pointing to a face or surface to be filleted</param>
        /// <param name="z">Coordinate Z pointing to a face or surface to be filleted</param>
        /// <param name="radius">filletPoint radius</param>
        private void CreateFaceFillet(Point3D point, double radius)
        {
            ksEntity filletEntity = AddFillet(radius);
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
        /// <param name="x">Coordinate X pointing to a face or surface to be filleted</param>
        /// <param name="y">Coordinate Y pointing to a face or surface to be filleted</param>
        /// <param name="z">Coordinate Z pointing to a face or surface to be filleted</param>
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
