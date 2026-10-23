# Dhivehi House 01 â€” rebuild 1.1.0

This is a versioned exterior-house rebuild for the existing Dhivehi House 01 listing. The original model source was not recovered. The rebuild retains its compact rectangular footprint, blue hipped roof, centered entry, and two principal front windows while adding continuous wall construction, modeled opening returns, deeper eaves, porch details, fitted doors, glazed windows, shutters, and finished rear and side elevations. The regional influence is an art direction; this is not a claim of historical accuracy.

## Unity use

- Tested editor: **Unity 6000.3.25f1**. Other editor versions and render pipelines are unverified.
- Import `Dhivehi_House_01_Unity_6000.3.25f1_v04.unitypackage`, then open `Assets/DhivehiHouse01/Demo/Dhivehi_House_01_Showcase.unity`.
- Prefab: `Assets/DhivehiHouse01/Prefabs/Dhivehi_House_01.prefab`.
- The root has identity rotation and rests at Unity Y=0. Each imported LOD branch uses a -90Â° X basis rotation to convert the Z-up Blender source to Unity Y-up. Visual meshes and LOD0 colliders move together. Unity bounds are 10.500 m wide Ã— 4.715 m high Ã— 10.005 m deep.
- LOD renderer counts are 41 / 7 / 7, with screen-relative thresholds 0.42 / 0.12 / 0.025. Eleven non-convex mesh colliders are assigned only to LOD0.
- The walkaround controller uses the legacy Input Manager. In **Edit > Project Settings > Player > Other Settings > Active Input Handling**, choose **Input Manager (Old)** or **Both**. Keep the default axes **Horizontal**, **Vertical**, **Mouse X**, and **Mouse Y**. With **Input System Package (New)** only, movement/look are inactive and an on-screen setup message is shown; the package does not change buyer ProjectSettings.
- The stone landing and three full-height step blocks meet ground level (Blender Z=0; Unity Y=0). Each adjacent tread overlaps 0.05 m in plan; the step tops rise 0.17 / 0.34 / 0.51 m to the 0.68 m landing. Matching LOD0 colliders match each block's measured bounds.
- Materials use the built-in Standard shader in the tested project. No URP/HDRP support is claimed.

## Editable source

- `exports/Dhivehi_House_01_v07.blend` is the Blender 5.1.2 scene with editable source meshes plus LOD1 and LOD2 collections. The source uses metric units (1 unit = 1 m), X for width, Y for depth, and +Z up. Its root is at the plan center and ground level. Ten 512Ã—512 texture maps are packed and supplied separately under `textures/`.
- Three FBX exports are included under `exports/`. Their material maps are also supplied in `textures/`.
- The Blender LOD0 source has 41 visual objects, 31,864 vertices, 31,268 faces, and 62,368 triangles, plus 11 collider meshes. Final evaluated FBX counts are in `MODEL_STATISTICS_v04.json`. These figures describe separate source and export representations.
- The FBX files carry authored tiling UV0 plus a separately regenerated `LightmapUV` after modifiers and final LOD joins/decimation. Clean FBX imports were checked for collapsed triangles and UV overlap using a 512Ã—512 triangle-interior occupancy raster per mesh; none were detected. Unity's generated secondary-UV option is disabled so the imported UV2 uses the authored channel.
- The visual materials are stucco, limestone, blue roof, teal shutters, walnut timber, dark hardware, and smoked glazing. Unity smoothness is converted as `1 - source roughness`; the metal scalars are retained. Five tiling base-color/normal-map pairs are included.
- The Blender source contains 17 editable hinge-origin empties with the door and shutter geometry parented beneath them; the FBX exports flatten those hierarchies and contain no hinge-pivot empties. The fitted door and shutters are static in the Unity prefab. Modeled hinge straps sit at the hinge-side edge and leaves cover their openings in the neutral pose. There are no animation clips or runtime interactions.
- The front door and windows occupy actual segmented wall openings with modeled returns and fitted panels/glazing. The package has no furnished or navigable interior; do not treat the exterior prop as an interior environment.

## Scope and limitations

This is an exterior environment prop with no furnished or navigable interior. Door and shutters do not animate at runtime. Unity behavior and package import were checked in the exact editor version above; compatibility with other versions or render pipelines is not asserted. The package adds no separate license terms; the product listing governs use.
