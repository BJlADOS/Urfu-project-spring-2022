
export function getSlotDateTime(slotDate: string): string {
  const date = new Date(slotDate)

  return date.toLocaleString('ru-RU', {
    hour: 'numeric',
    minute: 'numeric',
    day: 'numeric',
    month: 'long',
  })
}
