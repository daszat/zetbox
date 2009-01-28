
# Entities

Entities are created for each class, for each class of collection entry (one per N:M relation) and for each list of ValueTypes.

# Kinds of Associations

* Every 1:1 or 1:N ObjectReference: A -> B

    NewRelation.GetAll(ctx)
        .Where(r => r.GetPreferredStorage() != StorageHint.Separate)
        .OrderBy(r => r.GetAssociationName())

* N:M relations have two:  A -> Collection Entry, B -> Collection Entry

    NewRelation.GetAll(ctx)
        .Where(r => r.GetPreferredStorage() == StorageHint.Separate)
        .OrderBy(r => r.GetAssociationName())

* ValueType lists: Container (A) -> Value (B)

    ctx.GetQuery<ValueTypeProperty>()
        .Where(p => p.IsList)
        .OrderBy(p => p.ObjectClass.ClassName)
        .OrderBy(p => p.PropertyName)



# Misc notes

* make modifications first in CSDL and SSDL. MSL follows naturally and is bad place to make design decisions.