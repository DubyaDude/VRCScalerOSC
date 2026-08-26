using VRCScalerOSC.Service;

namespace VRCScalerOSC.Model
{
    public class OscEventCollection
    {
        private Dictionary<string, List<Action<bool, Service_VRCOSCProtocols?, OSCData>>>.AlternateLookup<ReadOnlySpan<char>> _events = new Dictionary<string, List<Action<bool, Service_VRCOSCProtocols?, OSCData>>>().GetAlternateLookup<ReadOnlySpan<char>>();

        public OscEventCollection()
        {
        }

        public void AddEvent(ReadOnlySpan<char> addr, Action<bool, Service_VRCOSCProtocols?, OSCData> action)
        {
            if (!_events.TryGetValue(addr, out var actions))
            {
                actions = new List<Action<bool, Service_VRCOSCProtocols?, OSCData>>();
                _events[addr] = actions;
            }
            actions.Add(action);
        }

        public bool TryExecuteEvent(ReadOnlySpan<char> addr, bool isInitialized, Service_VRCOSCProtocols? service, OSCData data)
        {
            bool executed = false;

            if (_events.TryGetValue(addr, out var actions))
            {
                foreach (var action in actions)
                {
                    action(isInitialized, service, data);
                    executed = true;
                }
            }

            return executed;
        }
    }
}
