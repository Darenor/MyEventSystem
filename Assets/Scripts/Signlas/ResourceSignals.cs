using deVoid.Utils;

public class AddResourceSignal : ASignal<ResourceType, int> { }

public class RemoveResourceSignal : ASignal<ResourceType, int> { }

public class ResetResourcesSignal : ASignal { }

public class UIStateSignal : ASignal<UIState> { }
