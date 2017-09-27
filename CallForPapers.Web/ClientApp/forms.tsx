import * as React from 'react'

interface TextCellProps<T> {
  form: T
  field: keyof T
  placeholder: string
  onFieldChange: (field: keyof T, newValue: T[keyof T]) => void
}

class TextCell<T> extends React.Component<TextCellProps<T>> {
  onChange = (e) => this.props.onFieldChange(this.props.field, e.currentTarget.value)
  render () {
    return (
      <input
        type="text"
        placeholder={this.props.placeholder}
        onChange={this.onChange}
        value={this.props.form[this.props.field].toString()}
      />
    )
  }
}

export function createTypedFormComponents<T> () {
  return {
    TextCell: TextCell as { new (): TextCell<T> }
  }
}
