namespace CSharp12;

public class SegmentTree
{
    private readonly int _originalLength;
    private readonly int[] _items;
    private readonly ISegmentTreeNodeMerger _merger;

    public SegmentTree(int[] nums, ISegmentTreeNodeMerger merger)
    {
        if (nums is null) throw new ArgumentNullException(nameof(nums));
        
        this._originalLength = nums.Length;
        this._items = new int[this._originalLength * 2];
        this._merger = merger ?? throw new ArgumentNullException(nameof(merger));

        Array.Copy(nums, sourceIndex: 0, this._items, destinationIndex: this._originalLength, length: this._originalLength);
        for (var i = this._originalLength - 1; i > 0; i--) this._items[i] = this._merger.Merge(this._items[i * 2], this._items[i * 2 + 1]);
    }
    
    // Other members are omitted for brevity
}

public interface ISegmentTreeNodeMerger
{
    int Merge(int leftValue, int rightValue);
    
    // Other members are omitted for brevity
}