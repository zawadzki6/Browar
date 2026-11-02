namespace Browar.API.World;

public static class Positioning {
    /// <summary> Remaps a given Vector3 + yaw to the room's new yaw</summary>
    public static Vector3 RemapByYaw(Vector3 offset, float yaw, Features.Room room) {
	float nya = room.Rotation.y;

	float delta = nya - yaw;
	Quaternion quaternion = Quaternion.Euler(0, delta, 0);
	return quaternion * offset;
    } 
    /// <summary> Gets a Vector3 offset with the room's new yaw</summary>
    /// <seealso cref="RemapByYaw(Vector3, float, Room)"/>
    public static Vector3 GetRoomOffset(Features.Room room, Vector3 offset, float yaw) => Features.Room.Get(room.Type).WorldPosition(RemapByYaw(offset, yaw, room));
    /// <summary> Gets a Vector3 offset with the room's new yaw</summary>
    /// <seealso cref="RemapByYaw(Vector3, float, Room)"/>
    public static Vector3 GetRoomOffset(RoomLocation rl) => Features.Room.Get(rl.room.Type).WorldPosition(RemapByYaw(rl.offset, rl.yaw, rl.room));

    /// <summary> Vector with yaw for a specified room</summary>
    public struct RoomLocation {
	public Features.Room room;
	public Vector3 offset;
	public float yaw;

	public RoomLocation(Features.Room ctor_room, Vector3 ctor_offset, float ctor_yaw) {
	    room = ctor_room;
	    offset = ctor_offset;
	    yaw = ctor_yaw;
	}
    }
}
